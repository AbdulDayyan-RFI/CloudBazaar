using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEB.Core.ViewModel;
using TEB.Core.Enumerations;
using TEB.Core.Domain;
using TEB.Core.Mapping;
using TEB.Data;


namespace TEB.Service.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IGenericRepository<Order> _Order;
        private readonly IGenericRepository<OrderItem> _OrderItem;
        private readonly IGenericRepository<Customer> _Customer;
        private readonly IGenericRepository<ShoppingCartItem> _shoppingCart;
        private readonly IGenericRepository<Product> _Product;
        private readonly IProductService _productService;
        private readonly IAddressService _addressService;
        public OrderService(IGenericRepository<Order> _Order, IGenericRepository<Customer> _Customer,
            IGenericRepository<ShoppingCartItem> _shoppingCart, IGenericRepository<OrderItem> _OrderItem,
            IGenericRepository<Product> _Product, IProductService productService,
            IAddressService addressService)
        {
            this._Order = _Order;
            this._Customer = _Customer;
            this._shoppingCart = _shoppingCart;
            this._Product = _Product;
            this._OrderItem = _OrderItem;
            this._productService = productService;
            this._addressService = addressService;
        }

        public List<CustomerOrderViewModel> GetOrders(int CustomerID)
        {
            string Query = @"select ord.Id,ord.OrderStatusId,ord.CreatedOnUtc,ord.OrderTotal,adr.Address1,adr.City,adr.ZipPostalCode
                            from [dbo].[Order] as ord 
                            join [dbo].[CustomerAddresses] cadr on ord.CustomerId = cadr.[Customer_Id] 
                            join [dbo].[Address] adr on cadr.[Address_Id] = adr.Id
                            where ord.CustomerId = @custid";
            var param = new { custid = CustomerID };
            var Data = _Order.Get<Order>(Query, param).ToList();
            List<CustomerOrderViewModel> Model = new List<CustomerOrderViewModel>();
            foreach (var item in Data)
            {
                CustomerOrderViewModel Obj = new CustomerOrderViewModel();
                Obj.OrderID = item.Id;
                Obj.OrderStatus = Enum.GetName(typeof(OrderStatusEnum), item.OrderStatusId);
                Obj.OrderDate = item.CreatedOnUtc;
                Obj.OrderPrice = (double)item.OrderTotal;
                Model.Add(Obj);
            }
            return Model;
        }

        public CustomerOrderViewModel GetSingleOrders(int OrderID)
        {
            string Query = @"select * from [dbo].[Order] where id = @orderid";
            var param = new { orderid = OrderID };
            var Data = _Order.Get<Order>(Query, param).FirstOrDefault();
            CustomerOrderViewModel Model = new CustomerOrderViewModel();
            Model.ShipmentMethod = Data.ShippingMethod;
            Model.ShipmentStatus = Enum.GetName(typeof(ShipmentStatusEnum), Data.ShippingStatusId);
            Model.OrderPrice = (double)Data.OrderTotal;
            Model.OrderID = OrderID;
            Model.OrderStatus = Enum.GetName(typeof(OrderStatusEnum), Data.OrderStatusId);
            Model.OrderDate = Data.CreatedOnUtc;
            Model.OrderPrice = (double)Data.OrderTotal;
            Model.BillingAddress = AddressMapping.ModelToViewModel(_addressService.GetAddressById(Data.BillingAddressId).Result);
            Model.ShippingAddress = AddressMapping.ModelToViewModel(_addressService.GetAddressById(Data.ShippingAddressId).Result);
            Model.PaymentStatus= Enum.GetName(typeof(PaymentStatusEnum), Data.PaymentStatusId);
            Model.PaymentMethod = Data.PaymentMethodSystemName;
            Model.ShipmentMethod = Data.ShippingMethod;
            Model.ShipmentStatus= Enum.GetName(typeof(ShipmentStatusEnum), Data.ShippingStatusId);

            string Query1 = @"select p.Id as ProductId,p.Name as ProductName,pic.PictureBinary,oi.UnitPriceInclTax as Price,oi.Quantity from [dbo].[Order] as o join
                           OrderItem as oi on o.Id = oi.OrderId join Product as p on oi.ProductId = p.Id join Product_Picture_Mapping as pm on p.Id = pm.ProductId
                           join Picture as pic on pic.Id = pm.PictureId where o.Id = @orderid";
            Model.ProductsList = _Order.Get<CustomerShoppingCartViewModel>(Query1, param).ToList();
            return Model;
        }

        public CustomerOrderViewModel GetSingleOrder(int OrderID)
        {
            string Query = @"select * from [dbo].[Order] where id = @orderid";
            var param = new { orderid = OrderID };
            var Data = _Order.Get<Order>(Query, param).FirstOrDefault();
            CustomerOrderViewModel Model = new CustomerOrderViewModel();
            Model.ShipmentMethod = Data.ShippingMethod;
            Model.ShipmentStatus = Enum.GetName(typeof(ShipmentStatusEnum), Data.ShippingStatusId);
            Model.OrderPrice = (double)Data.OrderTotal;
            Model.OrderID = OrderID;
            Model.OrderStatus = Enum.GetName(typeof(OrderStatusEnum), Data.OrderStatusId);
            Model.OrderDate = Data.CreatedOnUtc;
            Model.OrderPrice = (double)Data.OrderTotal;
            Model.BillingAddress = AddressMapping.ModelToViewModel(_addressService.GetAddressById(Data.BillingAddressId).Result);
            Model.ShippingAddress = AddressMapping.ModelToViewModel(_addressService.GetAddressById(Data.ShippingAddressId).Result);
            Model.PaymentStatus = Enum.GetName(typeof(PaymentStatusEnum), Data.PaymentStatusId);
            Model.PaymentMethod = Data.PaymentMethodSystemName;
            Model.ShipmentMethod = Data.ShippingMethod;
            Model.ShipmentStatus = Enum.GetName(typeof(ShipmentStatusEnum), Data.ShippingStatusId);

            string Query1 = @"select p.Name as ProductName,pic.PictureBinary,oi.UnitPriceInclTax as Price,oi.Quantity from [dbo].[Order] as o join
                           OrderItem as oi on o.Id = oi.OrderId join Product as p on oi.ProductId = p.Id join Product_Picture_Mapping as pm on p.Id = pm.ProductId
                           join Picture as pic on pic.Id = pm.PictureId where o.Id = @orderid";
            Model.ProductsList = _Order.Get<CustomerShoppingCartViewModel>(Query1, param).ToList();
            return Model;
        }

        //public object PlaceOreder(int Customerid)
        //{
        //    string query = @"select * from customer where Id = @customerid";
        //    var param = new { @customerid = Customerid };
        //    var customedetails = _Customer.Get<Customer>(query, param).FirstOrDefault();


        //    string ScQuery = @"select Product.Name as ProductName,Product.Price,Product.FullDescription as Description,ShoppingCartItem.Quantity,ShoppingCartItem.ProductId,Picture.PictureBinary from ShoppingCartItem 
        //                    join Product on ShoppingCartItem.ProductId = Product.Id
        //                    join Product_Picture_Mapping on Product.Id  = Product_Picture_Mapping.PictureId
        //                    join Picture on Picture.Id = Product_Picture_Mapping.PictureId
        //                    where ShoppingCartItem.CustomerId=@custid";
        //    var Params = new { custid = Customerid };
        //    var CustomerShoppingCart = _shoppingCart.Get<CustomerShoppingCartViewModel>(ScQuery, Params).ToList();


        //    string Query = @"INSERT INTO [dbo].[Order]([StoreId],[CustomerId],[BillingAddressId],[ShippingAddressId],[OrderStatusId],[ShippingStatusId],[PaymentStatusId],
        //                   [OrderTotal],[ShippingMethod],[CreatedOnUtc] VALUES
        //                    (@storeid,@customerid,@baddress,saddress,@orderstatus,@shippingstatus,@paymentstatus,@ordertotal,@shipmentmethod,@CreatedOnUtc)";
        //    var parameters = new
        //    {
        //        storeid = 1,
        //        customerid = Customerid,
        //        baddress = customedetails.BillingAddress_Id,
        //        saddress = customedetails.ShippingAddress_Id,
        //        orderstatus = OrderStatusEnum.Pending,
        //        shippingstatus = ShipmentStatusEnum.ShippingNotRequired,
        //        paymentstatus = PaymentStatusEnum.Pending,
        //        ordertotal = CustomerShoppingCart.Sum(x => x.Price),
        //        shipmentmethod = "COD",
        //        CreatedOnUtc = DateTime.Now
        //    };
        //    var Order = _Order.Update(Query, parameters);

        //    string orderquery = @"select top(1) * from [dbo].[Order] WHERE CustomerId= @custid order by Id desc";
        //    var Paramater = new { @custid = Customerid };
        //    var OrderDetails = _Order.Get(orderquery, Paramater);

        //    foreach (var item in CustomerShoppingCart)
        //    {
        //        string OrderdetailQuery = @"INSERT INTO [dbo].[OrderItem]([OrderId],[ProductId],[Quantity],[UnitPriceInclTax],[UnitPriceExclTax],[PriceInclTax],
        //                                  [PriceExclTax],[DownloadCount],[LicenseDownloadId],[ItemWeight])
        //                                  VALUES
        //                                  (@orderid,@productid,@qty,@unitprice@unitpriceexcl,@price,@priceexcl,@downloadcount,@licencedownload,@itemweight)";


        //        string Productquery = @"select * from Product where Id = @productid";
        //        var productparam = new { productid = item.ProductId };
        //        var productDetails = _Product.Get(Productquery, productparam);

        //        var newparam = new
        //        {
        //            orderid = OrderDetails.Id,
        //            productid = item.ProductId,
        //            qty = item.Quantity,
        //            unitprice = item.Price,
        //            unitpriceexcl = item.Price,
        //            price = item.Price,
        //            priceexcl = item.Price,
        //            downloadcount = 0,
        //            licencedownload = 0,
        //            itemweight = productDetails.Weight,
        //        };
        //        _OrderItem.Get(OrderdetailQuery, newparam);
        //    }

        //    return Order;
        //}


        public void PlaceOrder(int customerId)
        {
            string custquery = @"select * from customer where Id = @customerid";
            var custparam = new { @customerid = customerId };
            var customedetails = _Customer.Get(custquery, custparam);

            string cartQuery = @"select * from ShoppingCartItem where customerid=@customerid and ShoppingCartTypeId=@ShoppingCartTypeId and storeid=@storeid";

            var cartparam = new { customerid = customerId, ShoppingCartTypeId = (int)ShoppingCartTypeEnum.ShoppingCart, storeid = customedetails.RegisteredInStoreId };
            var cartDetails = _shoppingCart.GetAll(cartQuery, cartparam);




            //foreach (var item in cartDetails)
            //{
            //    GetSubTotal(item);
            //}


            string query = @"INSERT INTO [dbo].[Order] (
                                [OrderGuid],[StoreId],[CustomerId],[BillingAddressId],
                                [ShippingAddressId],[PickupAddressId],[PickUpInStore],[OrderStatusId],
                                [ShippingStatusId],[PaymentStatusId],[PaymentMethodSystemName],[CustomerCurrencyCode],
                                [CurrencyRate],[CustomerTaxDisplayTypeId],[VatNumber],[OrderSubtotalInclTax],
                                [OrderSubtotalExclTax],[OrderSubTotalDiscountInclTax],[OrderSubTotalDiscountExclTax],[OrderShippingInclTax],
                                [OrderShippingExclTax],[PaymentMethodAdditionalFeeInclTax],[PaymentMethodAdditionalFeeExclTax],[TaxRates],
                                [OrderTax],[OrderDiscount],[OrderTotal],[RefundedAmount],
                                [RewardPointsHistoryEntryId],[CheckoutAttributeDescription],[CheckoutAttributesXml],[CustomerLanguageId],
                                [AffiliateId],[CustomerIp],[AllowStoringCreditCardNumber],[CardType],
                                [CardName],[CardNumber],[MaskedCreditCardNumber],[CardCvv2],
                                [CardExpirationMonth],[CardExpirationYear],[AuthorizationTransactionId],[AuthorizationTransactionCode],
                                [AuthorizationTransactionResult],[CaptureTransactionId],[CaptureTransactionResult],[SubscriptionTransactionId],
                                [PaidDateUtc],[ShippingMethod],[ShippingRateComputationMethodSystemName],[CustomValuesXml],
                                [Deleted],[CreatedOnUtc],[CustomOrderNumber]) 
                                VALUES (
                                @OrderGuid, @StoreId, @CustomerId, @BillingAddressId,
                                @ShippingAddressId, @PickupAddressId, @PickUpInStore, @OrderStatusId, 
                                @ShippingStatusId, @PaymentStatusId, @PaymentMethodSystemName, @CustomerCurrencyCode, 
                                @CurrencyRate, @CustomerTaxDisplayTypeId, @VatNumber, @OrderSubtotalInclTax,
                                @OrderSubtotalExclTax, @OrderSubTotalDiscountInclTax, @OrderSubTotalDiscountExclTax, @OrderShippingInclTax,
                                @OrderShippingExclTax, @PaymentMethodAdditionalFeeInclTax, @PaymentMethodAdditionalFeeExclTax, @TaxRates,
                                @OrderTax, @OrderDiscount, @OrderTotal, @RefundedAmount,
                                @RewardPointsHistoryEntryId, @CheckoutAttributeDescription, @CheckoutAttributesXml, @CustomerLanguageId,
                                @AffiliateId, @CustomerIp, @AllowStoringCreditCardNumber, @CardType,
                                @CardName, @CardNumber, @MaskedCreditCardNumber, @CardCvv2,
                                @CardExpirationMonth, @CardExpirationYear, @AuthorizationTransactionId, @AuthorizationTransactionCode,
                                @AuthorizationTransactionResult, @CaptureTransactionId, @CaptureTransactionResult, @SubscriptionTransactionId,
                                @PaidDateUtc, @ShippingMethod, @ShippingRateComputationMethodSystemName, @CustomValuesXml,
                                @Deleted, @CreatedOnUtc, @CustomOrderNumber) SELECT CAST(SCOPE_IDENTITY() as int)";
            var param = new
            {
                OrderGuid = Guid.NewGuid(),
                StoreId = customedetails.RegisteredInStoreId,
                CustomerId = customerId,
                BillingAddressId = customedetails.BillingAddress_Id,
                ShippingAddressId = customedetails.ShippingAddress_Id,
                PickupAddressId = customedetails.ShippingAddress_Id,
                PickUpInStore = false,
                OrderStatusId = (int)OrderStatusEnum.Pending,
                ShippingStatusId = (int)ShipmentStatusEnum.NotYetShipped,
                PaymentStatusId = (int)PaymentStatusEnum.Paid,
                PaymentMethodSystemName = "",
                CustomerCurrencyCode = "USD",
                CurrencyRate = decimal.One,
                CustomerTaxDisplayTypeId = (int)TaxDisplayType.ExcludingTax,
                VatNumber = "",
                OrderSubtotalInclTax = decimal.Zero,
                OrderSubtotalExclTax = decimal.Zero,
                OrderSubTotalDiscountInclTax = decimal.Zero,
                OrderSubTotalDiscountExclTax = decimal.Zero,
                OrderShippingInclTax = decimal.Zero,
                OrderShippingExclTax = decimal.Zero,
                PaymentMethodAdditionalFeeInclTax = decimal.Zero,
                PaymentMethodAdditionalFeeExclTax = decimal.Zero,
                TaxRates = "0:0;",
                OrderTax = decimal.Zero,
                OrderDiscount = decimal.Zero,
                OrderTotal = decimal.Zero,
                RefundedAmount = decimal.Zero,
                RewardPointsHistoryEntryId = 0,
                CheckoutAttributeDescription = "",
                CheckoutAttributesXml = "",
                CustomerLanguageId = 1,
                AffiliateId = 0,
                CustomerIp = "",
                AllowStoringCreditCardNumber = false,
                CardType = "",
                CardName = "",
                CardNumber = "",
                MaskedCreditCardNumber = "",
                CardCvv2 = "",
                CardExpirationMonth = "",
                CardExpirationYear = "",
                AuthorizationTransactionId = "",
                AuthorizationTransactionCode = "",
                AuthorizationTransactionResult = "",
                CaptureTransactionId = "",
                CaptureTransactionResult = "",
                SubscriptionTransactionId = "",
                PaidDateUtc = DateTime.Now,
                ShippingMethod = "",
                ShippingRateComputationMethodSystemName = "",
                CustomValuesXml = "",
                Deleted = false,
                CreatedOnUtc = DateTime.Now,
                CustomOrderNumber = string.Empty
            };

            var order = _Order.Add(query, param);

            foreach (var item in cartDetails)
            {
                string orderItemquery = @"INSERT INTO [dbo].[OrderItem] ([OrderItemGuid] ,[OrderId] ,[ProductId] ,[Quantity] ,[UnitPriceInclTax] ,[UnitPriceExclTax] ,[PriceInclTax] ,[PriceExclTax] ,[DiscountAmountInclTax] ,[DiscountAmountExclTax] ,[OriginalProductCost] ,[AttributeDescription] ,[AttributesXml] ,[DownloadCount] ,[IsDownloadActivated] ,[LicenseDownloadId] ,[ItemWeight] ,[RentalStartDateUtc] ,[RentalEndDateUtc]) 
                                    VALUES (@OrderItemGuid, @OrderId, @ProductId, @Quantity, @UnitPriceInclTax, @UnitPriceExclTax, @PriceInclTax, @PriceExclTax, @DiscountAmountInclTax, @DiscountAmountExclTax, @OriginalProductCost, @AttributeDescription, @AttributesXml, @DownloadCount, @IsDownloadActivated, @LicenseDownloadId, @ItemWeight, @RentalStartDateUtc, @RentalEndDateUtc)";
                var product = _productService.GetProductById(item.ProductId);

                var orderItemParam = new
                {
                    OrderItemGuid = Guid.NewGuid(),
                    OrderId = order,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPriceInclTax = product.Price,
                    UnitPriceExclTax = product.Price,
                    PriceInclTax = product.Price,
                    PriceExclTax = product.Price,
                    DiscountAmountInclTax = 0,
                    DiscountAmountExclTax = 0,
                    OriginalProductCost = product.ProductCost,
                    AttributeDescription = "",
                    AttributesXml = "",
                    DownloadCount = "",
                    IsDownloadActivated = "",
                    LicenseDownloadId = "",
                    ItemWeight = product.Weight,
                    RentalStartDateUtc = "",
                    RentalEndDateUtc = "",
                };
                _OrderItem.Add(orderItemquery, orderItemParam);
                _shoppingCart.Delete("delete from ShoppingCartItem where customerid=@customerid and ShoppingCartTypeId=1 and Id=@scid", new { customerid = customerId, scid = item.Id });
            }


        }


        public Product GetProductById(int productId)
        {
            return _productService.GetProductById(productId);
        }

        public virtual decimal GetSubTotal(ShoppingCartItem shoppingCartItem,
            bool includeDiscounts = true)
        {
            //decimal discountAmount;
            //List<DiscountForCaching> appliedDiscounts;
            //int? maximumDiscountQty;
            //return GetSubTotal(shoppingCartItem, includeDiscounts, out discountAmount, out appliedDiscounts, out maximumDiscountQty); actual 
            return GetSubTotal(shoppingCartItem);
        }

        public virtual decimal GetSubTotal(ShoppingCartItem shoppingCartItem)
        {
            decimal subTotal;
            var unitPrice = GetUnitPrice(shoppingCartItem);
            return 0;
        }

        public virtual decimal GetUnitPrice(ShoppingCartItem shoppingCartItem)
        {
            if (shoppingCartItem == null)
                throw new ArgumentNullException("shoppingCartItem");
            Product product = GetProductById(shoppingCartItem.ProductId);
            GetUnitPrice(product);
            return 0;
        }


        public virtual decimal GetUnitPrice(Product product)
        {
            decimal finalPrice;
            return 0;

        }
    }
}
;