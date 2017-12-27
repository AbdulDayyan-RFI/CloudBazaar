using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEB.Core.Common;
using TEB.Core.Domain;
using TEB.Core.Enumerations;
using TEB.Core.ViewModel;
using TEB.Core.Helpers;
using TEB.Data;
using System.Threading;
using ImageResizer;

namespace TEB.Service
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<Picture> _pictureRepository;
        private const int MULTIPLE_THUMB_DIRECTORIES_LENGTH = 3;
        MediaSettings _mediaSettings;
        public ProductService(IGenericRepository<Product> productRepository, IGenericRepository<Picture> pictureRepository)
        {
            _productRepository = productRepository;
            _pictureRepository = pictureRepository;

            _mediaSettings = new MediaSettings
            {
                AvatarPictureSize = 120,
                ProductThumbPictureSize = 415,
                ProductDetailsPictureSize = 550,
                ProductThumbPictureSizeOnProductDetailsPage = 100,
                AssociatedProductPictureSize = 220,
                CategoryThumbPictureSize = 450,
                ManufacturerThumbPictureSize = 420,
                VendorThumbPictureSize = 450,
                CartThumbPictureSize = 80,
                MiniCartThumbPictureSize = 70,
                AutoCompleteSearchThumbPictureSize = 20,
                ImageSquarePictureSize = 32,
                MaximumImageSize = 1980,
                DefaultPictureZoomEnabled = false,
                DefaultImageQuality = 80,
                MultipleThumbDirectories = false,
                ImportProductImagesUsingHash = true,
                AzureCacheControlHeader = string.Empty
            };
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

        public virtual ProductDetailsModel PrepareProductDetailsModel(Product product,
            ShoppingCartItem updatecartitem = null, bool isAssociatedProduct = false)
        {
            ProductDetailsModel model = new ProductDetailsModel();
            var productName = product.Name;

            string pictureQuery = @"select p.Id,p.PictureBinary,p.MimeType,p.SeoFilename,p.SeoFilename,p.TitleAttribute,p.IsNew from Picture p join Product_Picture_Mapping pp on p.Id=pp.PictureId 
                                    where pp.ProductId=@productId order by pp.DisplayOrder,pp.id";
            var param = new { productId = product.Id };
            var picturelist = _pictureRepository.Get<Picture>(pictureQuery, param).ToList();
            var defaultPictureSize = isAssociatedProduct ?
            _mediaSettings.AssociatedProductPictureSize :
            _mediaSettings.ProductDetailsPictureSize;
            var defaultPicture = picturelist.FirstOrDefault();
            var defaultPictureModel = new PictureModel
            {
                ImageUrl = GetPictureUrl(defaultPicture, defaultPictureSize, !isAssociatedProduct),
                FullSizeImageUrl = GetPictureUrl(defaultPicture, 0, !isAssociatedProduct)
            };

            //"title" attribute
            defaultPictureModel.Title = (defaultPicture != null && !string.IsNullOrEmpty(defaultPicture.TitleAttribute)) ?
                defaultPicture.TitleAttribute :
                string.Format("Media.Product.ImageLinkTitleFormat.Details", productName);
            //"alt" attribute
            defaultPictureModel.AlternateText = (defaultPicture != null && !string.IsNullOrEmpty(defaultPicture.AltAttribute)) ?
                defaultPicture.AltAttribute :
                string.Format("Media.Product.ImageAlternateTextFormat.Details", productName);
            var pictureModels = new List<PictureModel>();

            foreach (var picture in picturelist)
            {
                var pictureModel = new PictureModel
                {
                    ImageUrl = GetPictureUrl(picture, defaultPictureSize, !isAssociatedProduct),
                    ThumbImageUrl = GetPictureUrl(picture, _mediaSettings.ProductThumbPictureSizeOnProductDetailsPage),
                    FullSizeImageUrl = GetPictureUrl(picture),
                    Title = string.Format("Media.Product.ImageLinkTitleFormat.Details", productName),
                    AlternateText = string.Format("Media.Product.ImageAlternateTextFormat.Details", productName),
                };
                //"title" attribute
                pictureModel.Title = !string.IsNullOrEmpty(picture.TitleAttribute) ?
                    picture.TitleAttribute :
                    string.Format("Media.Product.ImageLinkTitleFormat.Details", productName);
                //"alt" attribute
                pictureModel.AlternateText = !string.IsNullOrEmpty(picture.AltAttribute) ?
                    picture.AltAttribute :
                    string.Format("Media.Product.ImageAlternateTextFormat.Details", productName);

                pictureModels.Add(pictureModel);
            }
            model.DefaultPictureModel = defaultPictureModel;
            model.PictureModels = pictureModels;
            return model;
        }

        public virtual string GetPictureUrl(Picture picture,
            int targetSize = 0,
            bool showDefaultPicture = true,
            string storeLocation = null,
            PictureType defaultPictureType = PictureType.Entity)
        {
            string url = string.Empty;
            byte[] pictureBinary = null;
            if (picture != null)
                pictureBinary = LoadPictureBinary(picture);
            if (picture == null || pictureBinary == null || pictureBinary.Length == 0)
            {
                if (showDefaultPicture)
                {
                    url = GetDefaultPictureUrl(targetSize, defaultPictureType, storeLocation);
                }
                return url;
            }

            if (picture.IsNew)
            {
                DeletePictureThumbs(picture);

                //we do not validate picture binary here to ensure that no exception ("Parameter is not valid") will be thrown
                picture = UpdatePicture(picture.Id,
                    pictureBinary,
                    picture.MimeType,
                    picture.SeoFilename,
                    picture.AltAttribute,
                    picture.TitleAttribute,
                    false,
                    false);
            }

            var seoFileName = picture.SeoFilename; // = GetPictureSeName(picture.SeoFilename); //just for sure

            string lastPart = GetFileExtensionFromMimeType(picture.MimeType);
            string thumbFileName;
            if (targetSize == 0)
            {
                thumbFileName = !String.IsNullOrEmpty(seoFileName)
                    ? string.Format("{0}_{1}.{2}", picture.Id.ToString("0000000"), seoFileName, lastPart)
                    : string.Format("{0}.{1}", picture.Id.ToString("0000000"), lastPart);
            }
            else
            {
                thumbFileName = !String.IsNullOrEmpty(seoFileName)
                    ? string.Format("{0}_{1}_{2}.{3}", picture.Id.ToString("0000000"), seoFileName, targetSize, lastPart)
                    : string.Format("{0}_{1}.{2}", picture.Id.ToString("0000000"), targetSize, lastPart);
            }
            string thumbFilePath = GetThumbLocalPath(thumbFileName);

            //the named mutex helps to avoid creating the same files in different threads,
            //and does not decrease performance significantly, because the code is blocked only for the specific file.
            using (var mutex = new Mutex(false, thumbFileName))
            {
                if (!GeneratedThumbExists(thumbFilePath, thumbFileName))
                {
                    mutex.WaitOne();

                    //check, if the file was created, while we were waiting for the release of the mutex.
                    if (!GeneratedThumbExists(thumbFilePath, thumbFileName))
                    {
                        byte[] pictureBinaryResized;

                        //resizing required
                        if (targetSize != 0)
                        {
                            using (var stream = new MemoryStream(pictureBinary))
                            {
                                Bitmap b = null;
                                try
                                {
                                    //try-catch to ensure that picture binary is really OK. Otherwise, we can get "Parameter is not valid" exception if binary is corrupted for some reasons
                                    b = new Bitmap(stream);
                                }
                                catch (ArgumentException exc)
                                {

                                }

                                if (b == null)
                                {
                                    //bitmap could not be loaded for some reasons
                                    return url;
                                }

                                using (var destStream = new MemoryStream())
                                {
                                    var newSize = CalculateDimensions(b.Size, targetSize);
                                    ImageBuilder.Current.Build(b, destStream, new ResizeSettings
                                    {
                                        Width = newSize.Width,
                                        Height = newSize.Height,
                                        Scale = ScaleMode.Both,
                                        Quality = _mediaSettings.DefaultImageQuality
                                    });
                                    pictureBinaryResized = destStream.ToArray();
                                    b.Dispose();
                                }
                            }
                        }
                        else
                        {
                            //create a copy of pictureBinary
                            pictureBinaryResized = pictureBinary.ToArray();
                        }

                        SaveThumb(thumbFilePath, thumbFileName, picture.MimeType, pictureBinaryResized);
                    }

                    mutex.ReleaseMutex();
                }

            }
            url = GetThumbUrl(thumbFileName, storeLocation);
            return url;
        }

        protected virtual byte[] LoadPictureBinary(Picture picture)
        {
            if (picture == null)
                throw new ArgumentNullException("picture");
            bool fromDb = false;
            var result = fromDb
                ? picture.PictureBinary
                : LoadPictureFromFile(picture.Id, picture.MimeType);
            return result;
        }

        protected virtual byte[] LoadPictureFromFile(int pictureId, string mimeType)
        {
            string lastPart = GetFileExtensionFromMimeType(mimeType);
            string fileName = string.Format("{0}_0.{1}", pictureId.ToString("0000000"), lastPart);
            var filePath = GetPictureLocalPath(fileName);
            if (!File.Exists(filePath))
                return new byte[0];
            return File.ReadAllBytes(filePath);
        }

        protected virtual string GetFileExtensionFromMimeType(string mimeType)
        {
            if (mimeType == null)
                return null;

            //also see System.Web.MimeMapping for more mime types

            string[] parts = mimeType.Split('/');
            string lastPart = parts[parts.Length - 1];
            switch (lastPart)
            {
                case "pjpeg":
                    lastPart = "jpg";
                    break;
                case "x-png":
                    lastPart = "png";
                    break;
                case "x-icon":
                    lastPart = "ico";
                    break;
            }
            return lastPart;
        }

        protected virtual string GetPictureLocalPath(string fileName)
        {
            return Path.Combine(HtmlExtensions.MapPath("~/content/images/"), fileName);
        }

        public virtual string GetDefaultPictureUrl(int targetSize = 0,
             PictureType defaultPictureType = PictureType.Entity,
             string storeLocation = null)
        {
            string defaultImageFileName;
            switch (defaultPictureType)
            {
                case PictureType.Avatar:
                    defaultImageFileName = "default-avatar.jpg";// _settingService.GetSettingByKey("Media.Customer.DefaultAvatarImageName", "default-avatar.jpg");
                    break;
                case PictureType.Entity:
                default:
                    defaultImageFileName = "default-image.png";//_settingService.GetSettingByKey("Media.DefaultImageName", "default-image.png");
                    break;
            }
            string filePath = GetPictureLocalPath(defaultImageFileName);
            if (!File.Exists(filePath))
            {
                return "";
            }


            if (targetSize == 0)
            {
                string url = (!String.IsNullOrEmpty(storeLocation)
                                 ? storeLocation
                                 : ""//_webHelper.GetStoreLocation()
                                 )
                                 + "content/images/" + defaultImageFileName;
                return url;
            }
            else
            {
                string fileExtension = Path.GetExtension(filePath);
                string thumbFileName = string.Format("{0}_{1}{2}",
                    Path.GetFileNameWithoutExtension(filePath),
                    targetSize,
                    fileExtension);
                var thumbFilePath = GetThumbLocalPath(thumbFileName);
                if (!GeneratedThumbExists(thumbFilePath, thumbFileName))
                {
                    using (var b = new Bitmap(filePath))
                    {
                        using (var destStream = new MemoryStream())
                        {
                            var newSize = CalculateDimensions(b.Size, targetSize);
                            ImageBuilder.Current.Build(b, destStream, new ResizeSettings
                            {
                                Width = newSize.Width,
                                Height = newSize.Height,
                                Scale = ScaleMode.Both,
                                Quality = _mediaSettings.DefaultImageQuality
                            });
                            var destBinary = destStream.ToArray();
                            SaveThumb(thumbFilePath, thumbFileName, "", destBinary);
                        }
                    }
                }
                var url = GetThumbUrl(thumbFileName, storeLocation);
                return url;
            }
        }
        protected virtual void DeletePictureThumbs(Picture picture)
        {
            string filter = string.Format("{0}*.*", picture.Id.ToString("0000000"));
            var thumbDirectoryPath = HtmlExtensions.MapPath("~/content/images/thumbs");
            string[] currentFiles = System.IO.Directory.GetFiles(thumbDirectoryPath, filter, SearchOption.AllDirectories);
            foreach (string currentFileName in currentFiles)
            {
                var thumbFilePath = GetThumbLocalPath(currentFileName);
                File.Delete(thumbFilePath);
            }
        }

        public virtual Picture UpdatePicture(int pictureId, byte[] pictureBinary, string mimeType,
    string seoFilename, string altAttribute = null, string titleAttribute = null,
    bool isNew = true, bool validateBinary = true)
        {
            mimeType = HtmlExtensions.EnsureNotNull(mimeType);
            mimeType = HtmlExtensions.EnsureMaximumLength(mimeType, 20);

            seoFilename = HtmlExtensions.EnsureMaximumLength(seoFilename, 100);

            if (validateBinary)
                pictureBinary = ValidatePicture(pictureBinary, mimeType);

            var picture = GetPictureById(pictureId);
            if (picture == null)
                return null;

            //delete old thumbs if a picture has been changed
            //if (seoFilename != picture.SeoFilename)
            //    DeletePictureThumbs(picture);

            //picture.PictureBinary = this.StoreInDb ? pictureBinary : new byte[0];
            //picture.MimeType = mimeType;
            //picture.SeoFilename = seoFilename;
            //picture.AltAttribute = altAttribute;
            //picture.TitleAttribute = titleAttribute;
            //picture.IsNew = isNew;

            //_pictureRepository.Update(picture);

            //if (!this.StoreInDb)
            //    SavePictureInFile(picture.Id, pictureBinary, mimeType);

            ////event notification
            //_eventPublisher.EntityUpdated(picture);

            return picture;
        }
        protected virtual string GetThumbLocalPath(string thumbFileName)
        {
            var thumbsDirectoryPath = HtmlExtensions.MapPath("~/content/images/thumbs");
            if (_mediaSettings.MultipleThumbDirectories)
            {
                //get the first two letters of the file name
                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(thumbFileName);
                if (fileNameWithoutExtension != null && fileNameWithoutExtension.Length > MULTIPLE_THUMB_DIRECTORIES_LENGTH)
                {
                    var subDirectoryName = fileNameWithoutExtension.Substring(0, MULTIPLE_THUMB_DIRECTORIES_LENGTH);
                    thumbsDirectoryPath = Path.Combine(thumbsDirectoryPath, subDirectoryName);
                    if (!System.IO.Directory.Exists(thumbsDirectoryPath))
                    {
                        System.IO.Directory.CreateDirectory(thumbsDirectoryPath);
                    }
                }
            }
            var thumbFilePath = Path.Combine(thumbsDirectoryPath, thumbFileName);
            return thumbFilePath;
        }
        protected virtual bool GeneratedThumbExists(string thumbFilePath, string thumbFileName)
        {
            return File.Exists(thumbFilePath);
        }

        protected virtual Size CalculateDimensions(Size originalSize, int targetSize,
          ResizeType resizeType = ResizeType.LongestSide, bool ensureSizePositive = true)
        {
            float width, height;

            switch (resizeType)
            {
                case ResizeType.LongestSide:
                    if (originalSize.Height > originalSize.Width)
                    {
                        // portrait
                        width = originalSize.Width * (targetSize / (float)originalSize.Height);
                        height = targetSize;
                    }
                    else
                    {
                        // landscape or square
                        width = targetSize;
                        height = originalSize.Height * (targetSize / (float)originalSize.Width);
                    }
                    break;
                case ResizeType.Width:
                    width = targetSize;
                    height = originalSize.Height * (targetSize / (float)originalSize.Width);
                    break;
                case ResizeType.Height:
                    width = originalSize.Width * (targetSize / (float)originalSize.Height);
                    height = targetSize;
                    break;
                default:
                    throw new Exception("Not supported ResizeType");
            }

            if (ensureSizePositive)
            {
                if (width < 1)
                    width = 1;
                if (height < 1)
                    height = 1;
            }

            return new Size((int)Math.Round(width), (int)Math.Round(height));
        }

        protected virtual void SaveThumb(string thumbFilePath, string thumbFileName, string mimeType, byte[] binary)
        {
            File.WriteAllBytes(thumbFilePath, binary);
        }

        protected virtual string GetThumbUrl(string thumbFileName, string storeLocation = null)
        {
            storeLocation = !String.IsNullOrEmpty(storeLocation)
                                    ? storeLocation
                                    : "";//_webHelper.GetStoreLocation();
            var url = storeLocation + "content/images/thumbs/";

            if (_mediaSettings.MultipleThumbDirectories)
            {
                //get the first two letters of the file name
                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(thumbFileName);
                if (fileNameWithoutExtension != null && fileNameWithoutExtension.Length > MULTIPLE_THUMB_DIRECTORIES_LENGTH)
                {
                    var subDirectoryName = fileNameWithoutExtension.Substring(0, MULTIPLE_THUMB_DIRECTORIES_LENGTH);
                    url = url + subDirectoryName + "/";
                }
            }

            url = url + thumbFileName;
            return url;
        }

        public virtual byte[] ValidatePicture(byte[] pictureBinary, string mimeType)
        {
            using (var destStream = new MemoryStream())
            {
                ImageBuilder.Current.Build(pictureBinary, destStream, new ResizeSettings
                {
                    MaxWidth = _mediaSettings.MaximumImageSize,
                    MaxHeight = _mediaSettings.MaximumImageSize,
                    Quality = _mediaSettings.DefaultImageQuality
                });
                return destStream.ToArray();
            }
        }

        public virtual Picture GetPictureById(int pictureId)
        {
            if (pictureId == 0)
                return null;
            string query = "select * from Picture where Id=@id";
            var param = new { id = pictureId };
            return _pictureRepository.Get(query, param);
        }

    }
}
