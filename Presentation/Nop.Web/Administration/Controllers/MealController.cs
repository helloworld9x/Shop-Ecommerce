using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Nop.Admin.Extensions;
using Nop.Admin.Models.Flights;
using Nop.Admin.Models.Flights.Addon;
using Nop.Core.Collections;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Flights;
using Nop.Services.Flights;
using Nop.Services.Security;
using Nop.Services.Stores;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Kendoui;

namespace Nop.Admin.Controllers
{
    public class MealController : BaseAdminController
    {
        private readonly IFlightProductService _flightProductService;
        private readonly IStoreService _storeService;
        private readonly IStoreMappingService _storeMappingService;
        private readonly IPermissionService _permissionService;

        public MealController(IFlightProductService flightProductService, IStoreService storeService, IStoreMappingService storeMappingService, IPermissionService permissionService)
        {
            _flightProductService = flightProductService;
            _storeService = storeService;
            _storeMappingService = storeMappingService;
            _permissionService = permissionService;
        }

        [NonAction]
        protected virtual void PrepareStoresMappingModel(FlightProductModel model, FlightProduct product, bool excludeProperties)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            model.AvailableStores = _storeService
                .GetAllStores()
                .Select(s => s.ToModel())
                .ToList();
            if (!excludeProperties)
            {
                if (product != null)
                {
                    model.SelectedStoreIds = _storeMappingService.GetStoresIdsWithAccess(product);
                }
            }
        }

        [NonAction]
        protected virtual void SaveStoreMappings(FlightProduct flight, FlightProductModel model)
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

        #region Meal 

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageFlightStatus))
                return AccessDeniedView();

            var model = new MealListModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult ProductList(DataSourceRequest command)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageFlightStatus))
                return AccessDeniedView();

            var flights = _flightProductService.GetAllMeals().ToHashSet();
            var gridModel = new DataSourceResult
            {
                Data = flights.Select(ToModel),
                Total = flights.Count()
            };

            return Json(gridModel);
        }

        public ActionResult Create()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageFlightStatus))
                return AccessDeniedView();

            var model = new MealModel();
            //Stores
            PrepareStoresMappingModel(model, null, false);
            //default values

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageFlightStatus))
                return AccessDeniedView();

            var product = _flightProductService.GetProductById(id);
            if (product == null)
                //No country found with the specified id
                return RedirectToAction("Index");

            var model = ToModel(product);

            //Stores
            PrepareStoresMappingModel(model, product, false);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Create(MealModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageCountries))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var meal = ToEntity(model);
                if (meal != null)
                {
                    var product = new Product
                    {
                        Name = meal.Name,
                        Price = meal.Price,
                        Deleted = true,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow
                    };

                    _flightProductService.InsertMeal(meal, product);
                    //Stores
                    SaveStoreMappings(meal, model);

                    SuccessNotification("Add a new record successful");
                    return continueEditing ? RedirectToAction("Edit", new { id = meal.Id }) : RedirectToAction("List");

                }
            }

            //If we got this far, something failed, redisplay form

            //Stores
            PrepareStoresMappingModel(model, null, true);
            return View(model);
        }

        [NonAction]
        private MealModel ToModel(FlightProduct product)
        {
            if (product != null)
            {
                var meal = new MealModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Code = product.Code,
                    Currency = product.Currency,
                    Active = product.Active,
                    LimitedToStores = product.LimitedToStores,
                    SubjectToAcl = product.SubjectToAcl,
                    Type = product.Type,
                    Price = product.Price,
                };

                //attributes
                if (!string.IsNullOrEmpty(product.Attributes))
                {
                    var mealAttributes = JsonConvert.DeserializeObject<MealAttributes>(product.Attributes);
                    meal.MealAttributes = mealAttributes;
                }

                return meal;
            }
            return null;
        }

        [NonAction]
        private FlightProduct ToEntity(MealModel model)
        {
            if (model != null)
            {
                var entity = new FlightProduct()
                {
                    Active = model.Active,
                    Code = model.Code,
                    Currency = model.Currency,
                    Deleted = model.Deleted,
                    Name = model.Name,
                    Id = model.Id,
                    Price = model.Price,
                    LimitedToStores = model.LimitedToStores,
                    SubjectToAcl = model.SubjectToAcl,
                };

                if (model.MealAttributes != null)
                {
                    entity.Attributes = JsonConvert.SerializeObject(model.MealAttributes);
                }

                return entity;
            }
            return null;
        }
        #endregion
    }
}