using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Nop.Admin.Controllers;
using Nop.Admin.Extensions;
using Nop.Admin.Models.Catalog;
using Nop.Admin.Models.Directory;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Plugin.Misc.Region.Domain;
using Nop.Plugin.Misc.Region.Models;
using Nop.Plugin.Misc.Region.Services;
using Nop.Services.Catalog;
using Nop.Services.Directory;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Shipping;
using Nop.Services.Stores;
using Nop.Services.Vendors;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Kendoui;

namespace Nop.Plugin.Misc.Region.Controllers
{
    public class MiscRegionController : BaseAdminController
    {
        
        private readonly IWorkContext _workContext;
        private readonly ILocalizationService _localizationService;
        private readonly ICategoryService _categoryService;
        private readonly IManufacturerService _manufacturerService;
        private readonly IStoreService _storeService;
        private readonly IShippingService _shippingService;
        private readonly IVendorService _vendorService;
        private readonly IProductService _productService;
        private readonly IPictureService _pictureService;
        private readonly ICountryService _countryService;
        private readonly IMiscRegionService _miscRegionService;

        public MiscRegionController( IWorkContext workContext,
            ILocalizationService localizationService, ICategoryService categoryService,
            IManufacturerService manufacturerService, IStoreService storeService, IShippingService shippingService,
            IVendorService vendorService, IProductService productService, IPictureService pictureService, ICountryService countryService, IMiscRegionService miscRegionService)
        {
            _workContext = workContext;
            _localizationService = localizationService;
            _categoryService = categoryService;
            _manufacturerService = manufacturerService;
            _storeService = storeService;
            _shippingService = shippingService;
            _vendorService = vendorService;
            _productService = productService;
            _pictureService = pictureService;
            _countryService = countryService;
            _miscRegionService = miscRegionService;
        }

        [NonAction]
        protected virtual List<int> GetChildCategoryIds(int parentCategoryId)
        {
            var categoriesIds = new List<int>();
            var categories = _categoryService.GetAllCategoriesByParentCategoryId(parentCategoryId, true);
            foreach (var category in categories)
            {
                categoriesIds.Add(category.Id);
                categoriesIds.AddRange(GetChildCategoryIds(category.Id));
            }
            return categoriesIds;
        }

        [ChildActionOnly]
        [AdminAuthorize]
        public ActionResult Configure()
        {


            var model = new ProductListModel {IsLoggedInAsVendor = _workContext.CurrentVendor != null};
            //a vendor should have access only to his products

            //categories
            model.AvailableCategories.Add(new SelectListItem
            {
                Text = _localizationService.GetResource("Admin.Common.All"),
                Value = "0"
            });
            var categories = _categoryService.GetAllCategories(showHidden: true);
            foreach (var c in categories)
                model.AvailableCategories.Add(new SelectListItem
                {
                    Text = c.GetFormattedBreadCrumb(categories),
                    Value = c.Id.ToString()
                });

            //manufacturers
            model.AvailableManufacturers.Add(new SelectListItem
            {
                Text = _localizationService.GetResource("Admin.Common.All"),
                Value = "0"
            });
            foreach (var m in _manufacturerService.GetAllManufacturers(showHidden: true))
                model.AvailableManufacturers.Add(new SelectListItem {Text = m.Name, Value = m.Id.ToString()});

            //stores
            model.AvailableStores.Add(new SelectListItem
            {
                Text = _localizationService.GetResource("Admin.Common.All"),
                Value = "0"
            });
            foreach (var s in _storeService.GetAllStores())
                model.AvailableStores.Add(new SelectListItem {Text = s.Name, Value = s.Id.ToString()});

            //warehouses
            model.AvailableWarehouses.Add(new SelectListItem
            {
                Text = _localizationService.GetResource("Admin.Common.All"),
                Value = "0"
            });
            foreach (var wh in _shippingService.GetAllWarehouses())
                model.AvailableWarehouses.Add(new SelectListItem {Text = wh.Name, Value = wh.Id.ToString()});

            //vendors
            model.AvailableVendors.Add(new SelectListItem
            {
                Text = _localizationService.GetResource("Admin.Common.All"),
                Value = "0"
            });
            foreach (var v in _vendorService.GetAllVendors(showHidden: true))
                model.AvailableVendors.Add(new SelectListItem {Text = v.Name, Value = v.Id.ToString()});

            //product types
            model.AvailableProductTypes = ProductType.SimpleProduct.ToSelectList(false).ToList();
            model.AvailableProductTypes.Insert(0,
                new SelectListItem {Text = _localizationService.GetResource("Admin.Common.All"), Value = "0"});

            //"published" property
            //0 - all (according to "ShowHidden" parameter)
            //1 - published only
            //2 - unpublished only
            model.AvailablePublishedOptions.Add(new SelectListItem
            {
                Text = _localizationService.GetResource("Admin.Catalog.Products.List.SearchPublished.All"),
                Value = "0"
            });
            model.AvailablePublishedOptions.Add(new SelectListItem
            {
                Text = _localizationService.GetResource("Admin.Catalog.Products.List.SearchPublished.PublishedOnly"),
                Value = "1"
            });
            model.AvailablePublishedOptions.Add(new SelectListItem
            {
                Text = _localizationService.GetResource("Admin.Catalog.Products.List.SearchPublished.UnpublishedOnly"),
                Value = "2"
            });

            return View("~/Plugins/Misc.Region/Views/MiscRegion/Configure.cshtml", model);
        }

        [HttpPost]
        public ActionResult ProductList(DataSourceRequest command, ProductListModel model)
        {
            //a vendor should have access only to his products
            if (_workContext.CurrentVendor != null)
            {
                model.SearchVendorId = _workContext.CurrentVendor.Id;
            }

            var categoryIds = new List<int> {model.SearchCategoryId};
            //include subcategories
            if (model.SearchIncludeSubCategories && model.SearchCategoryId > 0)
                categoryIds.AddRange(GetChildCategoryIds(model.SearchCategoryId));

            //0 - all (according to "ShowHidden" parameter)
            //1 - published only
            //2 - unpublished only
            bool? overridePublished = null;
            if (model.SearchPublishedId == 1)
                overridePublished = true;
            else if (model.SearchPublishedId == 2)
                overridePublished = false;

            var products = _productService.SearchProducts(
                categoryIds: categoryIds,
                manufacturerId: model.SearchManufacturerId,
                storeId: model.SearchStoreId,
                vendorId: model.SearchVendorId,
                warehouseId: model.SearchWarehouseId,
                productType: model.SearchProductTypeId > 0 ? (ProductType?) model.SearchProductTypeId : null,
                keywords: model.SearchProductName,
                pageIndex: command.Page - 1,
                pageSize: command.PageSize,
                showHidden: true,
                overridePublished: overridePublished
                );
            var gridModel = new DataSourceResult();
            gridModel.Data = products.Select(x =>
            {
                var productModel = x.ToModel();
                //little hack here:
                //ensure that product full descriptions are not returned
                //otherwise, we can get the following error if products have too long descriptions:
                //"Error during serialization or deserialization using the JSON JavaScriptSerializer. The length of the string exceeds the value set on the maxJsonLength property. "
                //also it improves performance
                productModel.FullDescription = "";

                //picture
                var defaultProductPicture = _pictureService.GetPicturesByProductId(x.Id, 1).FirstOrDefault();
                productModel.PictureThumbnailUrl = _pictureService.GetPictureUrl(defaultProductPicture, 75, true);
                //product type
                productModel.ProductTypeName = x.ProductType.GetLocalizedEnum(_localizationService, _workContext);
                //friendly stock qantity
                //if a simple product AND "manage inventory" is "Track inventory", then display
                if (x.ProductType == ProductType.SimpleProduct &&
                    x.ManageInventoryMethod == ManageInventoryMethod.ManageStock)
                    productModel.StockQuantityStr = x.GetTotalStockQuantity().ToString();
                return productModel;
            });
            gridModel.Total = products.TotalCount;

            return Json(gridModel);

        }
        public ActionResult ProductRestriction(int Id)
        {
            var model = new ConfigurationModel();
            var countries = _countryService.GetAllCountries(showHidden: true);
            var product = _productService.GetProductById(Id);
            model.ProductName = product.Name;
            model.Product = product;

            // get Country 
            foreach (var country in countries)
            {
                model.AvailableCountries.Add(new CountryModel
                {
                    Id = country.Id,
                    Name = country.Name
                });
            }
            // get from data 

            foreach (var country in countries)
            {

                bool restricted = _miscRegionService.CountryRestrictionExists(Id,country.Id);
                model.Restricted[country.Id] = new Dictionary<int, bool> {[Id] = restricted};

            }
            return View("~/Plugins/Misc.Region/Views/MiscRegion/ProductRestriction.cshtml",model);
        }
        [HttpPost]
        public ActionResult ProductRestriction(FormCollection form)
        {
            var countries = _countryService.GetAllCountries(showHidden: true);
            var productId = Convert.ToInt32(form["ProductId"]);

            //Get checkbox Country 
            string formKey = "restrict_" + productId;
            var countryIdsToRestrict = form[formKey] != null
                ? form[formKey].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList()
                : new List<int>();


            // Add / Remove Coutry Restriction
            foreach (var country in countries)
            {
                bool restrict = countryIdsToRestrict.Contains(country.Id);
                if (restrict)
                {
                    var insertCountryProduct = new ProductRegion
                    {
                        ProductId = productId,
                        RegionId = country.Id,
                    };

                    //check ProductRegion in database is exist
                    var productRegion = _miscRegionService.GetProductRegionbyId(productId, country.Id);
                    if (productRegion != null)continue;

                    _miscRegionService.InsertProductRestriction(insertCountryProduct);
                }
                else
                {

                  var productRegion = _miscRegionService.GetProductRegionbyId(productId, country.Id);
                  if (productRegion == null) continue;

                    _miscRegionService.RemoveProductRegionbyId(productRegion.Id);
                }
            }
            SuccessNotification(_localizationService.GetResource("Admin.Configuration.product.Restrictions.Updated"));
            return ProductRestriction(productId);
        }
    }
}
