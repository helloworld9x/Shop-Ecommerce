using System;
using System.Linq;
using System.Web.Mvc;
using Nop.Admin.Extensions;
using Nop.Admin.Models.Flights;
using Nop.Core.Collections;
using Nop.Core.Domain.Flights;
using Nop.Services.Flights;
using Nop.Services.Security;
using Nop.Services.Stores;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Kendoui;

namespace Nop.Admin.Controllers
{
    public class FlightStatusController : BaseAdminController
    {
        private readonly IFlightStatusService _flightStatusService;
        private readonly IStoreService _storeService;
        private readonly IStoreMappingService _storeMappingService;
        private readonly IPermissionService _permissionService;

        public FlightStatusController(IFlightStatusService flightStatusService, IStoreService storeService, IStoreMappingService storeMappingService, IPermissionService permissionService)
        {
            _flightStatusService = flightStatusService;
            _storeService = storeService;
            _storeMappingService = storeMappingService;
            _permissionService = permissionService;
        }

        #region Store Mapping

        [NonAction]
        protected virtual void PrepareStoresMappingModel(FlightStatusModel model, FlightStatus flight, bool excludeProperties)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            model.AvailableStores = _storeService
                .GetAllStores()
                .Select(s => s.ToModel())
                .ToList();
            if (!excludeProperties)
            {
                if (flight != null)
                {
                    model.SelectedStoreIds = _storeMappingService.GetStoresIdsWithAccess(flight);
                }
            }
        }

        [NonAction]
        protected virtual void SaveStoreMappings(FlightStatus flight, FlightStatusModel model)
        {
            var existingStoreMappings = _storeMappingService.GetStoreMappings(flight);
            var allStores = _storeService.GetAllStores();
            foreach (var store in allStores)
            {
                if (model.SelectedStoreIds != null && model.SelectedStoreIds.Contains(store.Id))
                {
                    //new store
                    if (existingStoreMappings.Count(sm => sm.StoreId == store.Id) == 0)
                        _storeMappingService.InsertStoreMapping(flight, store.Id);
                }
                else
                {
                    //remove store
                    var storeMappingToDelete = existingStoreMappings.FirstOrDefault(sm => sm.StoreId == store.Id);
                    if (storeMappingToDelete != null)
                        _storeMappingService.DeleteStoreMapping(storeMappingToDelete);
                }
            }
        }

        #endregion

        #region Flights 

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageFlightStatus))
                return AccessDeniedView();

            var model = new FlightStatusListModel();
            return View(model);
        }


        [HttpPost]
        public ActionResult FlightList(DataSourceRequest command)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageFlightStatus))
                return AccessDeniedView();

            var flights = _flightStatusService.GetAllFlightStatus().ToHashSet();
            var gridModel = new DataSourceResult
            {
                Data = flights.Select(x => x.ToModel()),
                Total = flights.Count()
            };

            return Json(gridModel);
        }

        public ActionResult Create()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageFlightStatus))
                return AccessDeniedView();

            var model = new FlightStatusModel();
            //Stores
            PrepareStoresMappingModel(model, null, false);
            //default values

            return View(model);
        }


        public ActionResult Edit(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageFlightStatus))
                return AccessDeniedView();

            var flight = _flightStatusService.GetFlightById(id);
            if (flight == null)
                //No country found with the specified id
                return RedirectToAction("Index");

            var model = flight.ToModel();

            //Stores
            PrepareStoresMappingModel(model, flight, false);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Create(FlightStatusModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageCountries))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var flight = model.ToEntity();
                _flightStatusService.InsertFLightStatus(flight);
              
                //Stores
                SaveStoreMappings(flight, model);

                SuccessNotification("Add a new record successful");
                return continueEditing ? RedirectToAction("Edit", new { id = flight.Id }) : RedirectToAction("List");
            }

            //If we got this far, something failed, redisplay form

            //Stores
            PrepareStoresMappingModel(model, null, true);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Edit(FlightStatusModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageFlightStatus))
                return AccessDeniedView();

            var flight = _flightStatusService.GetFlightById(model.Id);
            if (flight == null)
                //No country found with the specified id
                return RedirectToAction("List");

            if (ModelState.IsValid)
            {
                flight = model.ToEntity(flight);
                _flightStatusService.UpdateFLightStatus(flight);
                
                //Stores
                SaveStoreMappings(flight, model);

                SuccessNotification("Update the record successful");

                if (continueEditing)
                {
                    //selected tab
                    SaveSelectedTabIndex();

                    return RedirectToAction("Edit", new { id = flight.Id });
                }
                return RedirectToAction("List");
            }

            //If we got this far, something failed, redisplay form

            //Stores
            PrepareStoresMappingModel(model, flight, true);
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageFlightStatus))
                return AccessDeniedView();

            var flight = _flightStatusService.GetFlightById(id);
            if (flight == null)
                //No country found with the specified id
                return RedirectToAction("List");

            try
            {
               //todo : will add the delete function with relationship of flight status.
                _flightStatusService.DeleteFlightStatus(flight);

                SuccessNotification("Deleted Successful.");
                return RedirectToAction("List");
            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return RedirectToAction("Edit", new { id = flight.Id });
            }
        }
        #endregion
    }
}