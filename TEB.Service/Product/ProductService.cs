using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEB.Core.Common;
using TEB.Core.Domain;
using TEB.Core.Enumerations;
using TEB.Core.ViewModel;
using TEB.Data;

namespace TEB.Service
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> _productRepository;
        public ProductService(IGenericRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public object InsertProduct(Product model)
        {
            var query = @"INSERT [dbo].[Product] ([ProductTypeId], [ParentGroupedProductId], [VisibleIndividually], [Name], [ShortDescription], [FullDescription], 
                        [AdminComment], [ProductTemplateId], [VendorId], [ShowOnHomePage], [MetaKeywords], [MetaDescription], [MetaTitle], [AllowCustomerReviews],
                        [ApprovedRatingSum], [NotApprovedRatingSum], [ApprovedTotalReviews], [NotApprovedTotalReviews], [SubjectToAcl], [LimitedToStores], [Sku], 
                        [ManufacturerPartNumber],[Gtin], [IsGiftCard], [GiftCardTypeId], [OverriddenGiftCardAmount], [RequireOtherProducts], [RequiredProductIds], 
                        [AutomaticallyAddRequiredProducts], [IsDownload],[DownloadId], [UnlimitedDownloads], [MaxNumberOfDownloads], [DownloadExpirationDays], 
                        [DownloadActivationTypeId], [HasSampleDownload], [SampleDownloadId],[HasUserAgreement], [UserAgreementText], [IsRecurring], 
                        [RecurringCycleLength], [RecurringCyclePeriodId], [RecurringTotalCycles], [IsRental], [RentalPriceLength],[RentalPricePeriodId], [IsShipEnabled], 
                        [IsFreeShipping], [ShipSeparately], [AdditionalShippingCharge], [DeliveryDateId], [IsTaxExempt], [TaxCategoryId],
                        [IsTelecommunicationsOrBroadcastingOrElectronicServices], [ManageInventoryMethodId], [ProductAvailabilityRangeId], [UseMultipleWarehouses], 
                        [WarehouseId],[StockQuantity], [DisplayStockAvailability], [DisplayStockQuantity], [MinStockQuantity], [LowStockActivityId], 
                        [NotifyAdminForQuantityBelow], [BackorderModeId],[AllowBackInStockSubscriptions], [OrderMinimumQuantity], [OrderMaximumQuantity], 
                        [AllowedQuantities], [AllowAddingOnlyExistingAttributeCombinations], [NotReturnable],[DisableBuyButton], [DisableWishlistButton],
                        [AvailableForPreOrder], [PreOrderAvailabilityStartDateTimeUtc], [CallForPrice], [Price], [OldPrice], [ProductCost],[CustomerEntersPrice], 
                        [MinimumCustomerEnteredPrice], [MaximumCustomerEnteredPrice], [BasepriceEnabled], [BasepriceAmount], [BasepriceUnitId], [BasepriceBaseAmount],
                        [BasepriceBaseUnitId], [MarkAsNew], [MarkAsNewStartDateTimeUtc], [MarkAsNewEndDateTimeUtc], [HasTierPrices], [HasDiscountsApplied], [Weight], 
                        [Length], [Width],[Height], [AvailableStartDateTimeUtc], [AvailableEndDateTimeUtc], [DisplayOrder], [Published], [Deleted],
                        [CreatedOnUtc], [UpdatedOnUtc]) 
                        values(@ProductTypeId, @ParentGroupedProductId, @VisibleIndividually, @Name, @ShortDescription, @FullDescription, 
                        @AdminComment, @ProductTemplateId, @VendorId, @ShowOnHomePage, @MetaKeywords, @MetaDescription, @MetaTitle, @AllowCustomerReviews,
                        @ApprovedRatingSum, @NotApprovedRatingSum, @ApprovedTotalReviews, @NotApprovedTotalReviews, @SubjectToAcl, @LimitedToStores, @Sku, 
                        @ManufacturerPartNumber,@Gtin, @IsGiftCard, @GiftCardTypeId, @OverriddenGiftCardAmount, @RequireOtherProducts, @RequiredProductIds, 
                        @AutomaticallyAddRequiredProducts, @IsDownload,@DownloadId, @UnlimitedDownloads, @MaxNumberOfDownloads, @DownloadExpirationDays, 
                        @DownloadActivationTypeId, @HasSampleDownload, @SampleDownloadId,@HasUserAgreement, @UserAgreementText, @IsRecurring, 
                        @RecurringCycleLength, @RecurringCyclePeriodId, @RecurringTotalCycles, @IsRental, @RentalPriceLength,@RentalPricePeriodId, @IsShipEnabled, 
                        @IsFreeShipping, @ShipSeparately, @AdditionalShippingCharge, @DeliveryDateId, @IsTaxExempt, @TaxCategoryId,
                        @IsTelecommunicationsOrBroadcastingOrElectronicServices, @ManageInventoryMethodId, @ProductAvailabilityRangeId, @UseMultipleWarehouses, 
                        @WarehouseId,@StockQuantity, @DisplayStockAvailability, @DisplayStockQuantity, @MinStockQuantity, @LowStockActivityId, 
                        @NotifyAdminForQuantityBelow, @BackorderModeId,@AllowBackInStockSubscriptions, @OrderMinimumQuantity, @OrderMaximumQuantity, 
                        @AllowedQuantities, @AllowAddingOnlyExistingAttributeCombinations, @NotReturnable,@DisableBuyButton, @DisableWishlistButton,
                        @AvailableForPreOrder, @PreOrderAvailabilityStartDateTimeUtc, @CallForPrice, @Price, @OldPrice, @ProductCost,@CustomerEntersPrice, 
                        @MinimumCustomerEnteredPrice, @MaximumCustomerEnteredPrice, @BasepriceEnabled, @BasepriceAmount, @BasepriceUnitId, @BasepriceBaseAmount,
                        @BasepriceBaseUnitId, @MarkAsNew, @MarkAsNewStartDateTimeUtc, @MarkAsNewEndDateTimeUtc, @HasTierPrices, @HasDiscountsApplied, @Weight, 
                        @Length, @Width,@Height, @AvailableStartDateTimeUtc, @AvailableEndDateTimeUtc, @DisplayOrder, @Published, 0, 
                        GETUTCDATE(), GETUTCDATE()); SELECT SCOPE_IDENTITY()";
            var param = new
            {
                ProductTypeId = model.ProductTypeId,
                ParentGroupedProductId = model.ParentGroupedProductId,
                VisibleIndividually = model.VisibleIndividually,
                Name = model.Name,
                ShortDescription = model.ShortDescription,
                FullDescription = model.FullDescription,
                AdminComment = model.AdminComment,
                ProductTemplateId = model.ProductTemplateId,
                VendorId = model.VendorId,
                ShowOnHomePage = model.ShowOnHomePage,
                MetaKeywords = model.MetaKeywords,
                MetaDescription = model.MetaDescription,
                MetaTitle = model.MetaTitle,
                AllowCustomerReviews = model.AllowCustomerReviews,
                ApprovedRatingSum = model.ApprovedRatingSum,
                NotApprovedRatingSum = model.NotApprovedRatingSum,
                ApprovedTotalReviews = model.ApprovedTotalReviews,
                NotApprovedTotalReviews = model.NotApprovedTotalReviews,
                SubjectToAcl = model.SubjectToAcl,
                LimitedToStores = model.LimitedToStores,
                Sku = model.Sku,
                ManufacturerPartNumber = model.ManufacturerPartNumber,
                Gtin = model.Gtin,
                IsGiftCard = model.IsGiftCard,
                GiftCardTypeId = model.GiftCardTypeId,
                OverriddenGiftCardAmount = model.OverriddenGiftCardAmount,
                RequireOtherProducts = model.RequireOtherProducts,
                RequiredProductIds = model.RequiredProductIds,
                AutomaticallyAddRequiredProducts = model.AutomaticallyAddRequiredProducts,
                IsDownload = model.IsDownload,
                DownloadId = model.DownloadId,
                UnlimitedDownloads = model.UnlimitedDownloads,
                MaxNumberOfDownloads = model.MaxNumberOfDownloads,
                DownloadExpirationDays = model.DownloadExpirationDays,
                DownloadActivationTypeId = model.DownloadActivationTypeId,
                HasSampleDownload = model.HasSampleDownload,
                SampleDownloadId = model.SampleDownloadId,
                HasUserAgreement = model.HasUserAgreement,
                UserAgreementText = model.UserAgreementText,
                IsRecurring = model.IsRecurring,
                RecurringCycleLength = model.RecurringCycleLength,
                RecurringCyclePeriodId = model.RecurringCyclePeriodId,
                RecurringTotalCycles = model.RecurringTotalCycles,
                IsRental = model.IsRental,
                RentalPriceLength = model.RentalPriceLength,
                RentalPricePeriodId = model.RentalPricePeriodId,
                IsShipEnabled = model.IsShipEnabled,
                IsFreeShipping = model.IsFreeShipping,
                ShipSeparately = model.ShipSeparately,
                AdditionalShippingCharge = model.AdditionalShippingCharge,
                DeliveryDateId = model.DeliveryDateId,
                IsTaxExempt = model.IsTaxExempt,
                TaxCategoryId = model.TaxCategoryId,
                IsTelecommunicationsOrBroadcastingOrElectronicServices = model.IsTelecommunicationsOrBroadcastingOrElectronicServices,
                ManageInventoryMethodId = model.ManageInventoryMethodId,
                ProductAvailabilityRangeId = model.ProductAvailabilityRangeId,
                UseMultipleWarehouses = model.UseMultipleWarehouses,
                WarehouseId = model.WarehouseId,
                StockQuantity = model.StockQuantity,
                DisplayStockAvailability = model.DisplayStockAvailability,
                DisplayStockQuantity = model.DisplayStockQuantity,
                MinStockQuantity = model.MinStockQuantity,
                LowStockActivityId = model.LowStockActivityId,
                NotifyAdminForQuantityBelow = model.NotifyAdminForQuantityBelow,
                BackorderModeId = model.BackorderModeId,
                AllowBackInStockSubscriptions = model.AllowBackInStockSubscriptions,
                OrderMinimumQuantity = model.OrderMinimumQuantity,
                OrderMaximumQuantity = model.OrderMaximumQuantity,
                AllowedQuantities = model.AllowedQuantities,
                AllowAddingOnlyExistingAttributeCombinations = model.AllowAddingOnlyExistingAttributeCombinations,
                NotReturnable = model.NotReturnable,
                DisableBuyButton = model.DisableBuyButton,
                DisableWishlistButton = model.DisableWishlistButton,
                AvailableForPreOrder = model.AvailableForPreOrder,
                PreOrderAvailabilityStartDateTimeUtc = model.PreOrderAvailabilityStartDateTimeUtc,
                CallForPrice = model.CallForPrice,
                Price = model.Price,
                OldPrice = model.OldPrice,
                ProductCost = model.ProductCost,
                CustomerEntersPrice = model.CustomerEntersPrice,
                MinimumCustomerEnteredPrice = model.MinimumCustomerEnteredPrice,
                MaximumCustomerEnteredPrice = model.MaximumCustomerEnteredPrice,
                BasepriceEnabled = model.BasepriceEnabled,
                BasepriceAmount = model.BasepriceAmount,
                BasepriceUnitId = model.BasepriceUnitId,
                BasepriceBaseAmount = model.BasepriceBaseAmount,
                BasepriceBaseUnitId = model.BasepriceBaseUnitId,
                MarkAsNew = model.MarkAsNew,
                MarkAsNewStartDateTimeUtc = model.MarkAsNewStartDateTimeUtc,
                MarkAsNewEndDateTimeUtc = model.MarkAsNewEndDateTimeUtc,
                HasTierPrices = model.HasTierPrices,
                HasDiscountsApplied = model.HasDiscountsApplied,
                Weight = model.Weight,
                Length = model.Length,
                Width = model.Width,
                Height = model.Height,
                AvailableStartDateTimeUtc = model.AvailableStartDateTimeUtc,
                AvailableEndDateTimeUtc = model.AvailableEndDateTimeUtc,
                DisplayOrder = model.DisplayOrder,
                Published = model.Published,
                Deleted = model.Deleted
            };
            return _productRepository.Add(query, param);
        }

        public Product GetProductById(int Id)
        {
            string query = "select * from Product where Id = @Id and Deleted = 0";
            var param = new { Id = Id };
            var product = _productRepository.Get(query, param);
            return product;
        }

        public int UpdateProduct(Product model)
        {
            var query = @"UPDATE [dbo].[Product] SET ProductTypeId=@ProductTypeId,ParentGroupedProductId=@ParentGroupedProductId,VisibleIndividually=@VisibleIndividually,
                        Name=@Name,ShortDescription=@ShortDescription,FullDescription=@FullDescription,AdminComment=@AdminComment,ProductTemplateId=@ProductTemplateId,
                        VendorId=@VendorId,ShowOnHomePage=@ShowOnHomePage,MetaKeywords=@MetaKeywords,MetaDescription=@MetaDescription,MetaTitle=@MetaTitle,
                        AllowCustomerReviews=@AllowCustomerReviews,ApprovedRatingSum=@ApprovedRatingSum,NotApprovedRatingSum=@NotApprovedRatingSum,
                        ApprovedTotalReviews=@ApprovedTotalReviews,NotApprovedTotalReviews=@NotApprovedTotalReviews,SubjectToAcl=@SubjectToAcl,
                        LimitedToStores=@LimitedToStores,Sku=@Sku,ManufacturerPartNumber=@ManufacturerPartNumber,Gtin=@Gtin,IsGiftCard=@IsGiftCard,
                        GiftCardTypeId=@GiftCardTypeId,OverriddenGiftCardAmount=@OverriddenGiftCardAmount,RequireOtherProducts=@RequireOtherProducts,
                        RequiredProductIds=@RequiredProductIds,AutomaticallyAddRequiredProducts=@AutomaticallyAddRequiredProducts,IsDownload=@IsDownload,
                        DownloadId=@DownloadId,UnlimitedDownloads=@UnlimitedDownloads,MaxNumberOfDownloads=@MaxNumberOfDownloads,
                        DownloadExpirationDays=@DownloadExpirationDays,DownloadActivationTypeId=@DownloadActivationTypeId,HasSampleDownload=@HasSampleDownload,
                        SampleDownloadId=@SampleDownloadId,HasUserAgreement=@HasUserAgreement,UserAgreementText=@UserAgreementText,IsRecurring=@IsRecurring,
                        RecurringCycleLength=@RecurringCycleLength,RecurringCyclePeriodId=@RecurringCyclePeriodId,RecurringTotalCycles=@RecurringTotalCycles,
                        IsRental=@IsRental,RentalPriceLength=@RentalPriceLength,RentalPricePeriodId=@RentalPricePeriodId,IsShipEnabled=@IsShipEnabled,
                        IsFreeShipping=@IsFreeShipping,ShipSeparately=@ShipSeparately,AdditionalShippingCharge=@AdditionalShippingCharge,
                        DeliveryDateId=@DeliveryDateId,IsTaxExempt=@IsTaxExempt,TaxCategoryId=@TaxCategoryId,
                        IsTelecommunicationsOrBroadcastingOrElectronicServices=@IsTelecommunicationsOrBroadcastingOrElectronicServices,
                        ManageInventoryMethodId=@ManageInventoryMethodId,ProductAvailabilityRangeId=@ProductAvailabilityRangeId,
                        UseMultipleWarehouses=@UseMultipleWarehouses,WarehouseId=@WarehouseId,StockQuantity=@StockQuantity,
                        DisplayStockAvailability=@DisplayStockAvailability,DisplayStockQuantity=@DisplayStockQuantity,MinStockQuantity=@MinStockQuantity,
                        LowStockActivityId=@LowStockActivityId,NotifyAdminForQuantityBelow=@NotifyAdminForQuantityBelow,BackorderModeId=@BackorderModeId,
                        AllowBackInStockSubscriptions=@AllowBackInStockSubscriptions,OrderMinimumQuantity=@OrderMinimumQuantity,
                        OrderMaximumQuantity=@OrderMaximumQuantity,AllowedQuantities=@AllowedQuantities,
                        AllowAddingOnlyExistingAttributeCombinations=@AllowAddingOnlyExistingAttributeCombinations,NotReturnable=@NotReturnable,
                        DisableBuyButton=@DisableBuyButton,DisableWishlistButton=@DisableWishlistButton,AvailableForPreOrder=@AvailableForPreOrder,
                        PreOrderAvailabilityStartDateTimeUtc=@PreOrderAvailabilityStartDateTimeUtc,CallForPrice=@CallForPrice,Price=@Price,OldPrice=@OldPrice,
                        ProductCost=@ProductCost,CustomerEntersPrice=@CustomerEntersPrice,MinimumCustomerEnteredPrice=@MinimumCustomerEnteredPrice,
                        MaximumCustomerEnteredPrice=@MaximumCustomerEnteredPrice,BasepriceEnabled=@BasepriceEnabled,BasepriceAmount=@BasepriceAmount,
                        BasepriceUnitId=@BasepriceUnitId,BasepriceBaseAmount=@BasepriceBaseAmount,BasepriceBaseUnitId=@BasepriceBaseUnitId,MarkAsNew=@MarkAsNew,
                        MarkAsNewStartDateTimeUtc=@MarkAsNewStartDateTimeUtc,MarkAsNewEndDateTimeUtc=@MarkAsNewEndDateTimeUtc,HasTierPrices=@HasTierPrices,
                        HasDiscountsApplied=@HasDiscountsApplied,Weight=@Weight,Length=@Length,Width=@Width,Height=@Height,
                        AvailableStartDateTimeUtc=@AvailableStartDateTimeUtc,AvailableEndDateTimeUtc=@AvailableEndDateTimeUtc,DisplayOrder=@DisplayOrder,
                        Published=@Published,UpdatedOnUtc=GETUTCDATE() WHERE Id = @Id; SELECT SCOPE_IDENTITY();";
            var param = new
            {
                Id = model.Id,
                ProductTypeId = model.ProductTypeId,
                ParentGroupedProductId = model.ParentGroupedProductId,
                VisibleIndividually = model.VisibleIndividually,
                Name = model.Name,
                ShortDescription = model.ShortDescription,
                FullDescription = model.FullDescription,
                AdminComment = model.AdminComment,
                ProductTemplateId = model.ProductTemplateId,
                VendorId = model.VendorId,
                ShowOnHomePage = model.ShowOnHomePage,
                MetaKeywords = model.MetaKeywords,
                MetaDescription = model.MetaDescription,
                MetaTitle = model.MetaTitle,
                AllowCustomerReviews = model.AllowCustomerReviews,
                ApprovedRatingSum = model.ApprovedRatingSum,
                NotApprovedRatingSum = model.NotApprovedRatingSum,
                ApprovedTotalReviews = model.ApprovedTotalReviews,
                NotApprovedTotalReviews = model.NotApprovedTotalReviews,
                SubjectToAcl = model.SubjectToAcl,
                LimitedToStores = model.LimitedToStores,
                Sku = model.Sku,
                ManufacturerPartNumber = model.ManufacturerPartNumber,
                Gtin = model.Gtin,
                IsGiftCard = model.IsGiftCard,
                GiftCardTypeId = model.GiftCardTypeId,
                OverriddenGiftCardAmount = model.OverriddenGiftCardAmount,
                RequireOtherProducts = model.RequireOtherProducts,
                RequiredProductIds = model.RequiredProductIds,
                AutomaticallyAddRequiredProducts = model.AutomaticallyAddRequiredProducts,
                IsDownload = model.IsDownload,
                DownloadId = model.DownloadId,
                UnlimitedDownloads = model.UnlimitedDownloads,
                MaxNumberOfDownloads = model.MaxNumberOfDownloads,
                DownloadExpirationDays = model.DownloadExpirationDays,
                DownloadActivationTypeId = model.DownloadActivationTypeId,
                HasSampleDownload = model.HasSampleDownload,
                SampleDownloadId = model.SampleDownloadId,
                HasUserAgreement = model.HasUserAgreement,
                UserAgreementText = model.UserAgreementText,
                IsRecurring = model.IsRecurring,
                RecurringCycleLength = model.RecurringCycleLength,
                RecurringCyclePeriodId = model.RecurringCyclePeriodId,
                RecurringTotalCycles = model.RecurringTotalCycles,
                IsRental = model.IsRental,
                RentalPriceLength = model.RentalPriceLength,
                RentalPricePeriodId = model.RentalPricePeriodId,
                IsShipEnabled = model.IsShipEnabled,
                IsFreeShipping = model.IsFreeShipping,
                ShipSeparately = model.ShipSeparately,
                AdditionalShippingCharge = model.AdditionalShippingCharge,
                DeliveryDateId = model.DeliveryDateId,
                IsTaxExempt = model.IsTaxExempt,
                TaxCategoryId = model.TaxCategoryId,
                IsTelecommunicationsOrBroadcastingOrElectronicServices = model.IsTelecommunicationsOrBroadcastingOrElectronicServices,
                ManageInventoryMethodId = model.ManageInventoryMethodId,
                ProductAvailabilityRangeId = model.ProductAvailabilityRangeId,
                UseMultipleWarehouses = model.UseMultipleWarehouses,
                WarehouseId = model.WarehouseId,
                StockQuantity = model.StockQuantity,
                DisplayStockAvailability = model.DisplayStockAvailability,
                DisplayStockQuantity = model.DisplayStockQuantity,
                MinStockQuantity = model.MinStockQuantity,
                LowStockActivityId = model.LowStockActivityId,
                NotifyAdminForQuantityBelow = model.NotifyAdminForQuantityBelow,
                BackorderModeId = model.BackorderModeId,
                AllowBackInStockSubscriptions = model.AllowBackInStockSubscriptions,
                OrderMinimumQuantity = model.OrderMinimumQuantity,
                OrderMaximumQuantity = model.OrderMaximumQuantity,
                AllowedQuantities = model.AllowedQuantities,
                AllowAddingOnlyExistingAttributeCombinations = model.AllowAddingOnlyExistingAttributeCombinations,
                NotReturnable = model.NotReturnable,
                DisableBuyButton = model.DisableBuyButton,
                DisableWishlistButton = model.DisableWishlistButton,
                AvailableForPreOrder = model.AvailableForPreOrder,
                PreOrderAvailabilityStartDateTimeUtc = model.PreOrderAvailabilityStartDateTimeUtc,
                CallForPrice = model.CallForPrice,
                Price = model.Price,
                OldPrice = model.OldPrice,
                ProductCost = model.ProductCost,
                CustomerEntersPrice = model.CustomerEntersPrice,
                MinimumCustomerEnteredPrice = model.MinimumCustomerEnteredPrice,
                MaximumCustomerEnteredPrice = model.MaximumCustomerEnteredPrice,
                BasepriceEnabled = model.BasepriceEnabled,
                BasepriceAmount = model.BasepriceAmount,
                BasepriceUnitId = model.BasepriceUnitId,
                BasepriceBaseAmount = model.BasepriceBaseAmount,
                BasepriceBaseUnitId = model.BasepriceBaseUnitId,
                MarkAsNew = model.MarkAsNew,
                MarkAsNewStartDateTimeUtc = model.MarkAsNewStartDateTimeUtc,
                MarkAsNewEndDateTimeUtc = model.MarkAsNewEndDateTimeUtc,
                HasTierPrices = model.HasTierPrices,
                HasDiscountsApplied = model.HasDiscountsApplied,
                Weight = model.Weight,
                Length = model.Length,
                Width = model.Width,
                Height = model.Height,
                AvailableStartDateTimeUtc = model.AvailableStartDateTimeUtc,
                AvailableEndDateTimeUtc = model.AvailableEndDateTimeUtc,
                DisplayOrder = model.DisplayOrder,
                Published = model.Published
            };
            _productRepository.Update(query, param);
            return model.Id;
        }

        public int DeleteProduct(int Id)
        {
            var query = @"UPDATE [dbo].[Product] SET Deleted=1,UpdatedOnUtc=GETUTCDATE() WHERE Id = @Id";
            var param = new { Id = Id };
            return _productRepository.Delete(query, param);
        }

        public ProductsViewModel GetProductByName(string ProductName)
        {
            string query = @"select p.Id as ProductID,p.Name as ProductName,p.Price,pic.PictureBinary from Product as p
                            left outer join Product_Picture_Mapping as ppm on p.Id =ppm.PictureId
                            left outer join Picture as pic on ppm.PictureId = pic.Id  where p.Name = @name and p.Published = 1 and p.Deleted = 0";
            var param = new { name = ProductName };
            var product = _productRepository.Get<ProductsViewModel>(query, param).FirstOrDefault();
            return product;
        }

        public Task<IEnumerable<Product>> SearchProducts(SearchProductModel model)
        {
            //Access control list. Allowed customer roles
            var allowedCustomerRolesIds = ""; //_workContext.CurrentCustomer.GetCustomerRoleIds();

            //pass category identifiers as comma-delimited string
            string commaSeparatedCategoryIds = model.categoryIds == null ? "" : string.Join(",", model.categoryIds);


            //pass customer role identifiers as comma-delimited string
            string commaSeparatedAllowedCustomerRoleIds = string.Join(",", allowedCustomerRolesIds);


            //pass specification identifiers as comma-delimited string
            string commaSeparatedSpecIds = "";
            if (model.filteredSpecs != null)
            {
                ((List<int>)model.filteredSpecs).Sort();
                commaSeparatedSpecIds = string.Join(",", model.filteredSpecs);
            }

            //some databases don't support int.MaxValue
            if (model.pageSize == int.MaxValue)
                model.pageSize = int.MaxValue - 1;

            //prepare parameters
            var parameters = new DynamicParameters();
            parameters.Add("CategoryIds", commaSeparatedCategoryIds, DbType.String);
            parameters.Add("ManufacturerId", model.manufacturerId, DbType.Int32);
            parameters.Add("VendorId", model.vendorId, DbType.Int32);
            parameters.Add("WarehouseId", model.warehouseId, DbType.Int32);
            parameters.Add("ProductTypeId", model.productType.HasValue ? (object)model.productType.Value : Convert.ToInt32(ProductType.SimpleProduct), DbType.Int32);
            parameters.Add("VisibleIndividuallyOnly", model.visibleIndividuallyOnly, DbType.Int32);
            parameters.Add("MarkedAsNewOnly", model.markedAsNewOnly, DbType.Int32);
            parameters.Add("ProductTagId", model.productTagId, DbType.Int32);
            parameters.Add("FeaturedProducts", model.featuredProducts.HasValue ? (object)model.featuredProducts.Value : DBNull.Value, DbType.Boolean);
            parameters.Add("PriceMin", model.priceMin.HasValue ? (object)model.priceMin.Value : DBNull.Value, DbType.Decimal);
            parameters.Add("PriceMax", model.priceMax.HasValue ? (object)model.priceMax.Value : DBNull.Value, DbType.Decimal);
            parameters.Add("Keywords", model.keywords != null ? (object)model.keywords : DBNull.Value, DbType.String);
            parameters.Add("SearchDescriptions", model.searchDescriptions, DbType.Boolean);
            parameters.Add("SearchManufacturerPartNumber", model.searchManufacturerPartNumber, DbType.Boolean);
            parameters.Add("SearchSku", model.searchSku, DbType.Boolean);
            parameters.Add("SearchProductTags", model.searchProductTags, DbType.Boolean);
            parameters.Add("FilteredSpecs", commaSeparatedSpecIds, DbType.String);
            parameters.Add("OrderBy", (int)model.orderBy, DbType.Int32);
            parameters.Add("PageIndex", model.pageIndex, DbType.Int32);
            parameters.Add("PageSize", model.pageSize, DbType.Int32);
            parameters.Add("ShowHidden", model.showHidden, DbType.Boolean);
            parameters.Add("OverridePublished", model.overridePublished != null ? (object)model.overridePublished.Value : DBNull.Value, DbType.Boolean);

            parameters.Add("StoreId", 0, DbType.Int32);
            parameters.Add("UseFullTextSearch", false, DbType.Boolean);
            parameters.Add("FullTextMode", 5, DbType.Int32);
            parameters.Add("LanguageId", 0, DbType.Int32);
            parameters.Add("AllowedCustomerRoleIds", "", DbType.String);
            parameters.Add("LoadFilterableSpecificationAttributeOptionIds", false, DbType.Boolean);

            parameters.Add("FilterableSpecificationAttributeOptionIds", null, DbType.String, ParameterDirection.Output, int.MaxValue - 1, null, null);
            parameters.Add("TotalRecords", null, DbType.Int32, ParameterDirection.Output, null, null, null);

            //invoke stored procedure
            var products = _productRepository.SqlStoredProcedure("ProductLoadAllPaged", parameters);


            ////get filterable specification attribute option identifier
            //string filterableSpecificationAttributeOptionIdsStr = (pFilterableSpecificationAttributeOptionIds.Value != DBNull.Value) ? (string)pFilterableSpecificationAttributeOptionIds.Value : "";
            //if (loadFilterableSpecificationAttributeOptionIds &&
            //    !string.IsNullOrWhiteSpace(filterableSpecificationAttributeOptionIdsStr))
            //{
            //    filterableSpecificationAttributeOptionIds = filterableSpecificationAttributeOptionIdsStr
            //       .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
            //       .Select(x => Convert.ToInt32(x.Trim()))
            //       .ToList();
            //}

            ////return products
            //var Totals = parameters.Get<int>("TotalRecords");
            //int totalRecords = Convert.ToInt32(Totals);
            //return new PagedList<Product>(s, model.pageIndex, model.pageSize);
            return products;
        }
    }
}
