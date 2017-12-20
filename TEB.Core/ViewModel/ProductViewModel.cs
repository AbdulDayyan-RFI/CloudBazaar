using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TEB.Core.ViewModel
{
    public class ProductViewModel
    {
        public ProductViewModel()
        {
            AvailableProductTemplates = new List<SelectListItem>();
            ProductsTypesSupportedByProductTemplates = new Dictionary<int, IList<SelectListItem>>();
            AvailableVendors = new List<SelectListItem>();
            AvailableDeliveryDates = new List<SelectListItem>();
            AvailableProductAvailabilityRanges = new List<SelectListItem>();
            AvailableWarehouses = new List<SelectListItem>();
            ProductWarehouseInventoryModels = new List<ProductWarehouseInventoryModel>();
            AvailableTaxCategories = new List<SelectListItem>();
            AvailableBasepriceUnits = new List<SelectListItem>();
            AvailableBasepriceBaseUnits = new List<SelectListItem>();
            SelectedDiscountIds = new List<int>();
            AvailableDiscounts = new List<SelectListItem>();
            SelectedCategoryIds = new List<int>();
            AvailableCategories = new List<SelectListItem>();
            SelectedManufacturerIds = new List<int>();
            AvailableManufacturers = new List<SelectListItem>();
            SelectedStoreIds = new List<int>();
            AvailableStores = new List<SelectListItem>();
            SelectedCustomerRoleIds = new List<int>();
            AvailableCustomerRoles = new List<SelectListItem>();
            ProductTags = new List<string>();
            AddSpecificationAttributeModel = new AddProductSpecificationAttributeModel();
        }
        public int Id { get; set; }
        public int ProductTypeId { get; set; }
        public int ParentGroupedProductId { get; set; }
        public bool VisibleIndividually { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public string AdminComment { get; set; }
        public bool ShowOnHomePage { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        public bool AllowCustomerReviews { get; set; }
        public int ApprovedRatingSum { get; set; }
        public int NotApprovedRatingSum { get; set; }
        public int ApprovedTotalReviews { get; set; }
        public int NotApprovedTotalReviews { get; set; }
        public bool SubjectToAcl { get; set; }
        public bool LimitedToStores { get; set; }
        public string Sku { get; set; }
        public string ManufacturerPartNumber { get; set; }
        public string Gtin { get; set; }
        public bool IsGiftCard { get; set; }
        public int GiftCardTypeId { get; set; }
        public decimal? OverriddenGiftCardAmount { get; set; }
        public bool RequireOtherProducts { get; set; }
        public string RequiredProductIds { get; set; }
        public bool AutomaticallyAddRequiredProducts { get; set; }
        public bool IsDownload { get; set; }
        public int DownloadId { get; set; }
        public bool UnlimitedDownloads { get; set; }
        public int MaxNumberOfDownloads { get; set; }
        public int? DownloadExpirationDays { get; set; }
        public int DownloadActivationTypeId { get; set; }
        public bool HasSampleDownload { get; set; }
        public int SampleDownloadId { get; set; }
        public bool HasUserAgreement { get; set; }
        public string UserAgreementText { get; set; }
        public bool IsRecurring { get; set; }
        public int RecurringCycleLength { get; set; }
        public int RecurringCyclePeriodId { get; set; }
        public int RecurringTotalCycles { get; set; }
        public bool IsRental { get; set; }
        public int RentalPriceLength { get; set; }
        public int RentalPricePeriodId { get; set; }
        public bool IsShipEnabled { get; set; }
        public bool IsFreeShipping { get; set; }
        public bool ShipSeparately { get; set; }
        public decimal AdditionalShippingCharge { get; set; }
        public bool IsTaxExempt { get; set; }
        public bool IsTelecommunicationsOrBroadcastingOrElectronicServices { get; set; }
        public int ManageInventoryMethodId { get; set; }
        public bool UseMultipleWarehouses { get; set; }
        public int StockQuantity { get; set; }
        public int LastStockQuantity { get; set; }
        public bool DisplayStockAvailability { get; set; }
        public bool DisplayStockQuantity { get; set; }
        public int MinStockQuantity { get; set; }
        public int LowStockActivityId { get; set; }
        public int NotifyAdminForQuantityBelow { get; set; }
        public int BackorderModeId { get; set; }
        public bool AllowBackInStockSubscriptions { get; set; }
        public int OrderMinimumQuantity { get; set; }
        public int OrderMaximumQuantity { get; set; }
        public string AllowedQuantities { get; set; }
        public bool AllowAddingOnlyExistingAttributeCombinations { get; set; }
        public bool NotReturnable { get; set; }
        public bool DisableBuyButton { get; set; }
        public bool DisableWishlistButton { get; set; }
        public bool AvailableForPreOrder { get; set; }
        public DateTime? PreOrderAvailabilityStartDateTimeUtc { get; set; }
        public bool CallForPrice { get; set; }
        public decimal Price { get; set; }
        public decimal OldPrice { get; set; }
        public decimal ProductCost { get; set; }
        public bool CustomerEntersPrice { get; set; }
        public decimal MinimumCustomerEnteredPrice { get; set; }
        public decimal MaximumCustomerEnteredPrice { get; set; }
        public bool BasepriceEnabled { get; set; }
        public decimal BasepriceAmount { get; set; }
        public decimal BasepriceBaseAmount { get; set; }
        public bool MarkAsNew { get; set; }
        public DateTime? MarkAsNewStartDateTimeUtc { get; set; }
        public DateTime? MarkAsNewEndDateTimeUtc { get; set; }
        public bool HasTierPrices { get; set; }
        public bool HasDiscountsApplied { get; set; }
        public decimal Weight { get; set; }
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public DateTime? AvailableStartDateTimeUtc { get; set; }
        public DateTime? AvailableEndDateTimeUtc { get; set; }
        public int DisplayOrder { get; set; }
        public bool Published { get; set; }
        public bool Deleted { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }

        public string ProductTag { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string PictureBinary { get; set; }

        [UIHint("MultiSelect")]
        public IList<int> SelectedDiscountIds { get; set; }
        public IList<SelectListItem> AvailableDiscounts { get; set; }

        [UIHint("MultiSelect")]
        public IList<int> SelectedCategoryIds { get; set; }
        public IList<SelectListItem> AvailableCategories { get; set; }

        [UIHint("MultiSelect")]
        public IList<int> SelectedManufacturerIds { get; set; }
        public IList<SelectListItem> AvailableManufacturers { get; set; }

        [UIHint("MultiSelect")]
        public IList<int> SelectedStoreIds { get; set; }
        public IList<SelectListItem> AvailableStores { get; set; }

        [UIHint("MultiSelect")]
        public IList<int> SelectedCustomerRoleIds { get; set; }
        public IList<SelectListItem> AvailableCustomerRoles { get; set; }

        public string PrimaryStoreCurrencyCode { get; set; }
        public string BaseDimensionIn { get; set; }
        public string BaseWeightIn { get; set; }

        public int ProductTemplateId { get; set; }
        public IList<SelectListItem> AvailableProductTemplates { get; set; }
        //<product type ID, list of supported product template IDs>
        public Dictionary<int, IList<SelectListItem>> ProductsTypesSupportedByProductTemplates { get; set; }

        public bool IsLoggedInAsVendor { get; set; }
        public int VendorId { get; set; }
        public IList<SelectListItem> AvailableVendors { get; set; }

        public int DeliveryDateId { get; set; }
        public IList<SelectListItem> AvailableDeliveryDates { get; set; }

        public int ProductAvailabilityRangeId { get; set; }
        public IList<SelectListItem> AvailableProductAvailabilityRanges { get; set; }

        public int WarehouseId { get; set; }
        public IList<SelectListItem> AvailableWarehouses { get; set; }

        public IList<ProductWarehouseInventoryModel> ProductWarehouseInventoryModels { get; set; }

        public int TaxCategoryId { get; set; }
        public IList<SelectListItem> AvailableTaxCategories { get; set; }

        public int BasepriceUnitId { get; set; }
        public IList<SelectListItem> AvailableBasepriceUnits { get; set; }

        public int BasepriceBaseUnitId { get; set; }
        public IList<SelectListItem> AvailableBasepriceBaseUnits { get; set; }
        public string SeName { get; set; }
        public List<string> ProductTags { get; set; }

        public AddProductSpecificationAttributeModel AddSpecificationAttributeModel { get; set; }

        public object ProductTypeName { get; set; }
        public object AssociatedToProductId { get; set; }
        public object AssociatedToProductName { get; set; }
        public object StockQuantityStr { get; set; }
        public object PictureThumbnailUrl { get; set; }
        public object Locales { get; set; }
        public object AvailableProductAttributes { get; set; }
        public object AddPictureModel { get; set; }
        public object ProductPictureModels { get; set; }
        public object CopyProductModel { get; set; }
        public object ProductEditorSettingsModel { get; set; }
        public object StockQuantityHistory { get; set; }
        public object CustomProperties { get; set; }
    }

    public partial class ProductWarehouseInventoryModel
    {
        public int WarehouseId { get; set; }
        public string WarehouseName { get; set; }
        public bool WarehouseUsed { get; set; }
        public int StockQuantity { get; set; }
        public int ReservedQuantity { get; set; }
        public int PlannedQuantity { get; set; }
    }

    public partial class AddProductSpecificationAttributeModel
    {
        public AddProductSpecificationAttributeModel()
        {
            AvailableAttributes = new List<SelectListItem>();
            AvailableOptions = new List<SelectListItem>();
        }

        public int SpecificationAttributeId { get; set; }

        public int AttributeTypeId { get; set; }

        public int SpecificationAttributeOptionId { get; set; }

        [AllowHtml]
        public string CustomValue { get; set; }

        public bool AllowFiltering { get; set; }

        public bool ShowOnProductPage { get; set; }

        public int DisplayOrder { get; set; }

        public IList<SelectListItem> AvailableAttributes { get; set; }
        public IList<SelectListItem> AvailableOptions { get; set; }
    }

}
