using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TEB.Core.Common;
using TEB.Core.Domain;
using TEB.Core.Enumerations;
using TEB.Core.Helpers;
using TEB.Core.Mapping;
using TEB.Core.ViewModel;
using TEB.Web.Controllers;

namespace TEB.Web.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        public ActionResult Index()
        {
            ProductSearchViewModel model = new ProductSearchViewModel();

            model.IsLoggedInAsVendor = false;
            model.AllowVendorsToImportProducts = false;

            //categories
            model.AvailableCategories.Add(new SelectListItem { Text = "Select", Value = "0" });
            //var categories = SelectListHelper.GetCategoryList(_categoryService, _cacheManager, true);
            //foreach (var c in categories)
            //    model.AvailableCategories.Add(c);

            //manufacturers
            model.AvailableManufacturers.Add(new SelectListItem { Text = "Select", Value = "0" });
            //var manufacturers = SelectListHelper.GetManufacturerList(_manufacturerService, _cacheManager, true);
            //foreach (var m in manufacturers)
            //    model.AvailableManufacturers.Add(m);

            //stores
            model.AvailableStores.Add(new SelectListItem { Text = "Select", Value = "0" });
            //foreach (var s in _storeService.GetAllStores())
            //    model.AvailableStores.Add(new SelectListItem { Text = s.Name, Value = s.Id.ToString() });

            //warehouses
            model.AvailableWarehouses.Add(new SelectListItem { Text = "Select", Value = "0" });
            //foreach (var wh in _shippingService.GetAllWarehouses())
            //    model.AvailableWarehouses.Add(new SelectListItem { Text = wh.Name, Value = wh.Id.ToString() });

            //vendors
            model.AvailableVendors.Add(new SelectListItem { Text = "Select", Value = "0" });
            //var vendors = SelectListHelper.GetVendorList(_vendorService, _cacheManager, true);
            //foreach (var v in vendors)
            //    model.AvailableVendors.Add(v);

            //product types
            model.AvailableProductTypes = ProductType.SimpleProduct.ToSelectList(false).ToList();
            model.AvailableProductTypes.Insert(0, new SelectListItem { Text = "Select", Value = "0" });

            //"published" property
            //0 - all (according to "ShowHidden" parameter)
            //1 - published only
            //2 - unpublished only
            model.AvailablePublishedOptions.Add(new SelectListItem { Text = "All", Value = "0" });
            model.AvailablePublishedOptions.Add(new SelectListItem { Text = "Published only", Value = "1" });
            model.AvailablePublishedOptions.Add(new SelectListItem { Text = "Unpublished only", Value = "2" });

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> ProductList(DataSourceRequest command, ProductSearchViewModel model)
        {
            var categoryIds = model.SearchCategoryId > 0 ? new List<int> { model.SearchCategoryId } : null;
            ////include subcategories
            //if (model.SearchIncludeSubCategories && model.SearchCategoryId > 0)
            //    categoryIds.AddRange(GetChildCategoryIds(model.SearchCategoryId));

            bool? overridePublished = null;
            if (model.SearchPublishedId == 1)
                overridePublished = true;
            else if (model.SearchPublishedId == 2)
                overridePublished = false;

            SearchProductModel searchmodel = new SearchProductModel
            {
                categoryIds = categoryIds,
                manufacturerId = model.SearchManufacturerId,
                storeId = model.SearchStoreId,
                vendorId = model.SearchVendorId,
                warehouseId = model.SearchWarehouseId,
                productType = model.SearchProductTypeId > 0 ? (ProductType?)model.SearchProductTypeId : null,
                keywords = model.SearchProductName,
                pageIndex = command.Page - 1,
                pageSize = command.PageSize,
                showHidden = true,
                overridePublished = overridePublished
            };
            TEBApiResponse apiResponse = await Post<SearchProductModel>("/Product/SearchProducts", searchmodel);

            if (apiResponse.IsSuccess)
            {
                try
                {
                    List<Product> listproducts = JsonConvert.DeserializeObject<List<Product>>(Convert.ToString(apiResponse.Data));
                    PagedList<Product> products = new PagedList<Product>(listproducts, 0, 10);
                    var gridModel = new DataSourceResult();
                    gridModel.Data = products.Select(x =>
                    {
                        var productModel = ProductMapping.ModelToView(x);// x.ToModel();
                                                                         //little performance optimization: ensure that "FullDescription" is not returned
                        productModel.FullDescription = "";

                        ////picture
                        //var defaultProductPicture = _pictureService.GetPicturesByProductId(x.Id, 1).FirstOrDefault();
                        //productModel.PictureThumbnailUrl = _pictureService.GetPictureUrl(defaultProductPicture, 75, true);
                        ////product type
                        //productModel.ProductTypeName = x.ProductType.GetLocalizedEnum(_localizationService, _workContext);
                        ////friendly stock qantity
                        ////if a simple product AND "manage inventory" is "Track inventory", then display
                        //if (x.ProductType == ProductType.SimpleProduct && x.ManageInventoryMethod == ManageInventoryMethod.ManageStock)
                        //    productModel.StockQuantityStr = x.GetTotalStockQuantity().ToString();

                        return productModel;
                    });
                    gridModel.Total = products.TotalCount;

                    return Json(gridModel);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return Json(0);
        }

        public ActionResult Create()
        {
            var model = new ProductViewModel();

            PrepareProductModel(model, null, true, true);
            //AddLocales(_languageService, model.Locales);
            //PrepareAclModel(model, null, false);
            //PrepareStoresMappingModel(model, null, false);
            //PrepareCategoryMappingModel(model, null, false);
            //PrepareManufacturerMappingModel(model, null, false);
            //PrepareDiscountMappingModel(model, null, false);

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(ProductViewModel model, string savecontinue)
        {
            if (ModelState.IsValid)
            {
                bool continueEditing = false;
                if (!String.IsNullOrWhiteSpace(savecontinue))
                    continueEditing = true;

                //basic setting
                //this will be removed 
                DefaultValues(model);

                //product
                Product product = new Product();
                product = ProductMapping.ViewToModel(model);

                TEBApiResponse apiResponse = await Post<Product>("/Product/InsertProduct", product);

                ////search engine name
                //model.SeName = product.ValidateSeName(model.SeName, product.Name, true);
                //_urlRecordService.SaveSlug(product, model.SeName, 0);
                ////locales
                //UpdateLocales(product, model);
                //categories
                //SaveCategoryMappings(product, model);
                //manufacturers
                //SaveManufacturerMappings(product, model);
                ////ACL (customer roles)
                //SaveProductAcl(product, model);
                ////stores
                //SaveStoreMappings(product, model);
                ////discounts
                //SaveDiscountMappings(product, model);
                //tags
                //_productTagService.UpdateProductTags(product, ParseProductTags(model.ProductTags));
                ////warehouses
                //SaveProductWarehouseInventory(product, model);

                ////quantity change history
                //_productService.AddStockQuantityHistoryEntry(product, product.StockQuantity, product.StockQuantity, product.WarehouseId,
                //    _localizationService.GetResource("Admin.StockQuantityHistory.Messages.Edit"));

                ////activity log
                //_customerActivityService.InsertActivity("AddNewProduct", _localizationService.GetResource("ActivityLog.AddNewProduct"), product.Name);

                //SuccessNotification(_localizationService.GetResource("Admin.Catalog.Products.Added"));

                if (apiResponse.IsSuccess)
                {
                    if (continueEditing)
                    {
                        //selected tab
                        //SaveSelectedTabName();
                        int productid = JsonConvert.DeserializeObject<int>(Convert.ToString(apiResponse.Data));
                        return RedirectToAction("Edit", new { id = productid });
                    }
                    return RedirectToAction("Index");
                }
            }

            //If we got this far, something failed, redisplay form
            PrepareProductModel(model, null, false, true);
            //PrepareAclModel(model, null, true);
            //PrepareStoresMappingModel(model, null, true);
            //PrepareCategoryMappingModel(model, null, true);
            //PrepareManufacturerMappingModel(model, null, true);
            //PrepareDiscountMappingModel(model, null, true);

            return View(model);
        }

        public async Task<ActionResult> Edit(int id)
        {
            ProductViewModel model = new ProductViewModel();
            TEBApiResponse apiResponse = await Get("/Product/GetProductById?Id=" + id);
            if (apiResponse.IsSuccess)
            {
                Product product = JsonConvert.DeserializeObject<Product>(Convert.ToString(apiResponse.Data));
                model = ProductMapping.ModelToView(product);
            }

            if (model != null)
            {
                TEBApiResponse apiResponses = await Get("/SpecificationAttribute/GetSpecificationAttributes");
                if (apiResponses.IsSuccess)
                {
                    List<SpecificationAttribute> list = JsonConvert.DeserializeObject<List<SpecificationAttribute>>(Convert.ToString(apiResponses.Data));

                    model.AddSpecificationAttributeModel.AvailableAttributes = list.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }).ToList();
                }
            }


            PrepareProductModel(model, null, false, false);
            //AddLocales(_languageService, model.Locales);
            //PrepareAclModel(model, null, false);
            //PrepareStoresMappingModel(model, null, false);
            //PrepareCategoryMappingModel(model, null, false);
            //PrepareManufacturerMappingModel(model, null, false);
            //PrepareDiscountMappingModel(model, null, false);
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(ProductViewModel model, string savecontinue)
        {
            if (ModelState.IsValid)
            {
                bool continueEditing = false;
                if (!String.IsNullOrWhiteSpace(savecontinue))
                    continueEditing = true;

                //basic setting
                //this will be removed 
                DefaultValues(model);

                //product
                Product product = new Product();
                product = ProductMapping.ViewToModel(model);

                TEBApiResponse apiResponse = await Post<Product>("/Product/UpdateProduct", product);

                if (apiResponse.IsSuccess)
                {
                    if (continueEditing)
                    {
                        //selected tab
                        //SaveSelectedTabName();
                        int productid = JsonConvert.DeserializeObject<int>(Convert.ToString(apiResponse.Data));
                        return RedirectToAction("Edit", new { id = productid });
                    }
                    return RedirectToAction("Index");
                }
            }
            //If we got this far, something failed, redisplay form
            PrepareProductModel(model, null, false, true);
            //PrepareAclModel(model, null, true);
            //PrepareStoresMappingModel(model, null, true);
            //PrepareCategoryMappingModel(model, null, true);
            //PrepareManufacturerMappingModel(model, null, true);
            //PrepareDiscountMappingModel(model, null, true);

            return View(model);
        }

        public async Task<ActionResult> Delete(int id)
        {
            TEBApiResponse apiResponse = await Delete("/Product/DeleteProduct?Id=" + id);
            return RedirectToAction("Index");
        }

        [NonAction]
        protected virtual void PrepareProductModel(ProductViewModel model, Product product, bool setPredefinedValues, bool excludeProperties)
        {
            var productTagsSb = new StringBuilder();
            productTagsSb.Append("var initialProductTags = [");
            for (int i = 0; i < model.ProductTags.Count; i++)
            {
                var tag = model.ProductTags[i];
                productTagsSb.Append("'");
                productTagsSb.Append(HttpUtility.JavaScriptStringEncode(tag));
                productTagsSb.Append("'");
                if (i != model.ProductTags.Count() - 1)
                {
                    productTagsSb.Append(",");
                }
            }



            productTagsSb.Append("]");

            //model.PrimaryStoreCurrencyCode = _currencyService.GetCurrencyById(_currencySettings.PrimaryStoreCurrencyId).CurrencyCode;
            //model.BaseWeightIn = _measureService.GetMeasureWeightById(_measureSettings.BaseWeightId).Name;
            //model.BaseDimensionIn = _measureService.GetMeasureDimensionById(_measureSettings.BaseDimensionId).Name;

            //vendors
            //model.IsLoggedInAsVendor = _workContext.CurrentVendor != null;
            //model.AvailableVendors.Add(new SelectListItem
            //{
            //    Text = _localizationService.GetResource("Admin.Catalog.Products.Fields.Vendor.None"),
            //    Value = "0"
            //});
            //var vendors = SelectListHelper.GetVendorList(_vendorService, _cacheManager, true);
            //foreach (var v in vendors)
            //    model.AvailableVendors.Add(v);

            //warehouses
            //var warehouses = _shippingService.GetAllWarehouses();
            //model.AvailableWarehouses.Add(new SelectListItem
            //{
            //    Text = _localizationService.GetResource("Admin.Catalog.Products.Fields.Warehouse.None"),
            //    Value = "0"
            //});
            //foreach (var warehouse in warehouses)
            //{
            //    model.AvailableWarehouses.Add(new SelectListItem
            //    {
            //        Text = warehouse.Name,
            //        Value = warehouse.Id.ToString()
            //    });
            //}

            //multiple warehouses
            //foreach (var warehouse in warehouses)
            //{
            //    var pwiModel = new ProductModel.ProductWarehouseInventoryModel
            //    {
            //        WarehouseId = warehouse.Id,
            //        WarehouseName = warehouse.Name
            //    };
            //    if (product != null)
            //    {
            //        //var pwi = product.ProductWarehouseInventory.FirstOrDefault(x => x.WarehouseId == warehouse.Id);
            //        //if (pwi != null)
            //        //{
            //        //    pwiModel.WarehouseUsed = true;
            //        //    pwiModel.StockQuantity = pwi.StockQuantity;
            //        //    pwiModel.ReservedQuantity = pwi.ReservedQuantity;
            //        //    pwiModel.PlannedQuantity = _shipmentService.GetQuantityInShipments(product, pwi.WarehouseId, true, true);
            //        //}
            //    }
            //    model.ProductWarehouseInventoryModels.Add(pwiModel);
            //}

            ////baseprice units
            //var measureWeights = _measureService.GetAllMeasureWeights();
            //foreach (var mw in measureWeights)
            //    model.AvailableBasepriceUnits.Add(new SelectListItem { Text = mw.Name, Value = mw.Id.ToString(), Selected = product != null && !setPredefinedValues && mw.Id == product.BasepriceUnitId });
            //foreach (var mw in measureWeights)
            //    model.AvailableBasepriceBaseUnits.Add(new SelectListItem { Text = mw.Name, Value = mw.Id.ToString(), Selected = product != null && !setPredefinedValues && mw.Id == product.BasepriceBaseUnitId });


            if (product != null)
            {
                //var parentGroupedProduct = _productService.GetProductById(product.ParentGroupedProductId);
                //if (parentGroupedProduct != null)
                //{
                //    model.AssociatedToProductId = product.ParentGroupedProductId;
                //    model.AssociatedToProductName = parentGroupedProduct.Name;
                //}

                //model.CreatedOn = _dateTimeHelper.ConvertToUserTime(product.CreatedOnUtc, DateTimeKind.Utc);
                //model.UpdatedOn = _dateTimeHelper.ConvertToUserTime(product.UpdatedOnUtc, DateTimeKind.Utc);

                ////product attributes
                //foreach (var productAttribute in _productAttributeService.GetAllProductAttributes())
                //{
                //    model.AvailableProductAttributes.Add(new SelectListItem
                //    {
                //        Text = productAttribute.Name,
                //        Value = productAttribute.Id.ToString()
                //    });
                //}

                ////specification attributes
                //model.AddSpecificationAttributeModel.AvailableAttributes = _cacheManager
                //    .Get(ModelCacheEventConsumer.SPEC_ATTRIBUTES_MODEL_KEY, () =>
                //    {
                //        var availableSpecificationAttributes = new List<SelectListItem>();
                //        foreach (var sa in _specificationAttributeService.GetSpecificationAttributes())
                //        {
                //            availableSpecificationAttributes.Add(new SelectListItem
                //            {
                //                Text = sa.Name,
                //                Value = sa.Id.ToString()
                //            });
                //        }
                //        return availableSpecificationAttributes;
                //    });

                ////options of preselected specification attribute
                //if (model.AddSpecificationAttributeModel.AvailableAttributes.Any())
                //{
                //    var selectedAttributeId = int.Parse(model.AddSpecificationAttributeModel.AvailableAttributes.First().Value);
                //    foreach (var sao in _specificationAttributeService.GetSpecificationAttributeOptionsBySpecificationAttribute(selectedAttributeId))
                //        model.AddSpecificationAttributeModel.AvailableOptions.Add(new SelectListItem
                //        {
                //            Text = sao.Name,
                //            Value = sao.Id.ToString()
                //        });
                //}
                ////default specs values
                //model.AddSpecificationAttributeModel.ShowOnProductPage = true;

                ////copy product
                //model.CopyProductModel.Id = product.Id;
                //model.CopyProductModel.Name = string.Format(_localizationService.GetResource("Admin.Catalog.Products.Copy.Name.New"), product.Name);
                //model.CopyProductModel.Published = true;
                //model.CopyProductModel.CopyImages = true;

                //// product tags
                //var result = new StringBuilder();
                //for (int i = 0; i < product.ProductTags.Count; i++)
                //{
                //    var pt = product.ProductTags.ToList()[i];
                //    result.Append(pt.Name);
                //    if (i != product.ProductTags.Count - 1)
                //        result.Append(", ");
                //}
                //model.ProductTags = result.ToString();

                ////last stock quantity
                //model.LastStockQuantity = product.StockQuantity;
            }

            //default values
            if (setPredefinedValues)
            {
                model.MaximumCustomerEnteredPrice = 1000;
                model.MaxNumberOfDownloads = 10;
                model.RecurringCycleLength = 100;
                model.RecurringTotalCycles = 10;
                model.RentalPriceLength = 1;
                model.StockQuantity = 10000;
                model.NotifyAdminForQuantityBelow = 1;
                model.OrderMinimumQuantity = 1;
                model.OrderMaximumQuantity = 10000;
                model.TaxCategoryId = 0;
                model.UnlimitedDownloads = true;
                model.IsShipEnabled = true;
                model.AllowCustomerReviews = true;
                model.Published = true;
                model.VisibleIndividually = true;
            }
        }

        private static void DefaultValues(ProductViewModel model)
        {
            model.IsTaxExempt = false;
            model.TaxCategoryId = 1;

            model.ManageInventoryMethodId = 1;
            model.StockQuantity = 10;


            model.IsShipEnabled = false;
            model.Height = 0.0000M;
            model.Length = 0.0000M;
            model.Weight = 0.0000M;
            model.Width = 0.0000M;

            model.SelectedCategoryIds = new List<int>();

            model.AddPictureModel = null;
            model.AddSpecificationAttributeModel = null;
            model.AdminComment = null;
            model.AllowAddingOnlyExistingAttributeCombinations = false;
            model.AllowBackInStockSubscriptions = false;
            model.AllowCustomerReviews = true;
            model.AllowedQuantities = null;
            model.AssociatedToProductId = 0;
            model.AssociatedToProductName = null;
            model.AutomaticallyAddRequiredProducts = false;
            model.AvailableBasepriceBaseUnits = new List<SelectListItem>();
            model.AvailableBasepriceUnits = new List<SelectListItem>();
            model.AvailableCategories = new List<SelectListItem>();
            model.AvailableCustomerRoles = new List<SelectListItem>();
            model.AvailableDeliveryDates = new List<SelectListItem>();
            model.AvailableDiscounts = new List<SelectListItem>();
            model.AvailableEndDateTimeUtc = null;
            model.AvailableForPreOrder = false;
            model.AvailableManufacturers = new List<SelectListItem>();
            model.AvailableProductAttributes = new List<SelectListItem>();
            model.AvailableProductAvailabilityRanges = new List<SelectListItem>();
            model.AvailableProductTemplates = new List<SelectListItem>();
            model.AvailableStartDateTimeUtc = null;
            model.AvailableStores = new List<SelectListItem>();
            model.AvailableTaxCategories = new List<SelectListItem>();
            model.AvailableVendors = new List<SelectListItem>();
            model.AvailableWarehouses = new List<SelectListItem>();
            model.BackorderModeId = 0;
            model.BaseDimensionIn = null;
            model.BaseWeightIn = null;
            model.BasepriceAmount = 0;
            model.BasepriceBaseAmount = 0;
            model.BasepriceBaseUnitId = 1;
            model.BasepriceEnabled = false;
            model.BasepriceUnitId = 1;
            model.CallForPrice = false;
            model.CopyProductModel = null;
            model.CreatedOn = null;
            model.CustomProperties = null;
            model.CustomerEntersPrice = false;
            model.DeliveryDateId = 0;
            model.DisableBuyButton = false;
            model.DisableWishlistButton = false;
            model.DisplayOrder = 0;
            model.DisplayStockAvailability = false;
            model.DisplayStockQuantity = false;
            model.DownloadActivationTypeId = 0;
            model.DownloadExpirationDays = null;
            model.DownloadId = 0;
            model.AdditionalShippingCharge = 0.0000M;
            model.GiftCardTypeId = 0;
            model.Gtin = null;
            model.HasSampleDownload = false;
            model.HasUserAgreement = false;
            model.IsDownload = false;
            model.IsFreeShipping = false;
            model.IsGiftCard = false;
            model.IsLoggedInAsVendor = false;
            model.IsRecurring = false;
            model.IsRental = false;
            model.IsTelecommunicationsOrBroadcastingOrElectronicServices = false;
            model.LastStockQuantity = 0;
            model.Locales = null;
            model.LowStockActivityId = 0;
            model.ManufacturerPartNumber = null;
            model.MarkAsNew = false;
            model.MarkAsNewEndDateTimeUtc = null;
            model.MarkAsNewStartDateTimeUtc = null;
            model.MaxNumberOfDownloads = 10;
            model.MaximumCustomerEnteredPrice = 1000.0000M;
            model.MetaDescription = null;
            model.MetaKeywords = null;
            model.MetaTitle = null;
            model.MinStockQuantity = 0;
            model.MinimumCustomerEnteredPrice = 0.0000M;
            model.NotReturnable = false;
            model.NotifyAdminForQuantityBelow = 1;
            model.OldPrice = 0.0000M;
            model.OrderMaximumQuantity = 10000;
            model.OrderMinimumQuantity = 1;
            model.OverriddenGiftCardAmount = null;
            model.PictureThumbnailUrl = null;
            model.PreOrderAvailabilityStartDateTimeUtc = null;
            model.PrimaryStoreCurrencyCode = null;
            model.ProductAvailabilityRangeId = 0;
            model.ProductCost = 0.0000M;
            model.ProductPictureModels = null;
            model.ProductTags = null;
            model.ProductTemplateId = 1;
            model.ProductTypeId = 5;
            model.ProductTypeName = null;
            model.ProductWarehouseInventoryModels = null;
            model.ProductsTypesSupportedByProductTemplates = null;
            model.Published = true;
            model.RecurringCycleLength = 100;
            model.RecurringCyclePeriodId = 0;
            model.RecurringTotalCycles = 10;
            model.RentalPriceLength = 1;
            model.RentalPricePeriodId = 0;
            model.RequireOtherProducts = false;
            model.RequiredProductIds = null;
            model.SampleDownloadId = 0;
            model.SeName = null;
            model.StockQuantityHistory = null;
            model.StockQuantityStr = null;
            model.SelectedCustomerRoleIds = new List<int>();
            model.SelectedDiscountIds = new List<int>();
            model.SelectedManufacturerIds = new List<int>();
            model.SelectedStoreIds = new List<int>();
            model.ShipSeparately = false;
            model.ShowOnHomePage = false;
            model.UnlimitedDownloads = true;
            model.UpdatedOn = null;
            model.UseMultipleWarehouses = false;
            model.UserAgreementText = null;
            model.VendorId = 0;
            model.VisibleIndividually = true;
            model.WarehouseId = 0;
        }

        [HttpPost]
        public async Task<ActionResult> ProductSpecAttrList(DataSourceRequest command, int productId)
        {
            //if (!_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
            //    return AccessDeniedKendoGridJson();
            TEBApiResponse apiResponse = await Get("/Product/GetProductSpecificationAttributes?productId=" + productId);

            if (apiResponse.IsSuccess)
            {
                List<Product_SpecificationAttribute_Mapping> productSpecificationAttribute = JsonConvert.DeserializeObject<List<Product_SpecificationAttribute_Mapping>>(Convert.ToString(apiResponse.Data));
                var productrSpecsModel = productSpecificationAttribute
              .Select(x =>
              {
                  var psaModel = new Product_SpecificationAttribute_MappingViewModel
                  {
                      Id = x.Id,
                      AttributeTypeId = x.AttributeTypeId,
                     // AttributeTypeName = ,
                      //AttributeId = x.SpecificationAttributeOption.SpecificationAttribute.Id,
                      //AttributeName = x.SpecificationAttributeOption.SpecificationAttribute.Name,
                      AllowFiltering = x.AllowFiltering,
                      ShowOnProductPage = x.ShowOnProductPage,
                      DisplayOrder = x.DisplayOrder
                  };
                  SpecificationAttributeType AttributeType = ((SpecificationAttributeType)x.AttributeTypeId);

                  switch (AttributeType)
                  {
                      case SpecificationAttributeType.Option:
                          psaModel.ValueRaw = HttpUtility.HtmlEncode(Enum.Parse(typeof(SpecificationAttributeOption), x.SpecificationAttributeOptionId.ToString()));
                          psaModel.SpecificationAttributeOptionId = x.SpecificationAttributeOptionId;
                          break;
                      case SpecificationAttributeType.CustomText:
                          psaModel.ValueRaw = HttpUtility.HtmlEncode(x.CustomValue);
                          break;
                      case SpecificationAttributeType.CustomHtmlText:
                          //do not encode?
                          //psaModel.ValueRaw = x.CustomValue;
                          psaModel.ValueRaw = HttpUtility.HtmlEncode(x.CustomValue);
                          break;
                      case SpecificationAttributeType.Hyperlink:
                          psaModel.ValueRaw = x.CustomValue;
                          break;
                      default:
                          break;
                  }
                  return psaModel;
              })
              .ToList();

                var gridModel = new DataSourceResult
                {
                    Data = productrSpecsModel,
                    Total = productrSpecsModel.Count
                };
                return Json(gridModel);
            }

            return Json(0);

        }
    }
}
