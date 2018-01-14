using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEB.Core;
using TEB.Core.Common;
using TEB.Core.Domain;
using TEB.Core.ViewModel;
using TEB.Core.Enumerations;
using TEB.Data;

namespace TEB.Service
{
    public class ShoppingCartService : IShopingCartService
    {
        private readonly IGenericRepository<Address> _BillingAddressRepository;
        private readonly IGenericRepository<ShoppingCartItem> _ShoppingCartItemRepository;
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IProductService _productService;

        public ShoppingCartService(IGenericRepository<Address> BillingAddressRepository, IGenericRepository<ShoppingCartItem> ShoppingCartItemRepository,
            IGenericRepository<Product> productRepository, IProductService productService)
        {
            _BillingAddressRepository = BillingAddressRepository;
            _ShoppingCartItemRepository = ShoppingCartItemRepository;
            _productRepository = productRepository;
            _productService = productService;
        }

        public int InsertBillingAddress(Address model)
        {
            try
            {
                string query = @"INSERT [dbo].[Address] ([FirstName] ,[LastName] ,[Email] ,
                                      [Company],[CountryId],[StateprovinceId],[City],
                                      [Address1],[Address2],[ZipPostalCode],[PhoneNumber],
                                      [FaxNumber],[CustomAttributes],[CreatedDate],[UpdatedDate]
                                      ) VALUES(@FirstName, @LastName, @Email,
                                      @Company, @CountryId, @StateprovinceId, @City,
                                      @Address1, @Address2, @ZipPostalCode, @PhoneNumber,
                                      @FaxNumber, @CustomAttributes, GETUTCDATE(),
                                      GETUTCDATE()); SELECT SCOPE_IDENTITY()";
                var param = new Address
                {

                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Company = model.Company,
                    CountryId = model.CountryId,
                    StateprovinceId = model.StateprovinceId,
                    City = model.City,
                    Address1 = model.Address1,
                    Address2 = model.Address2,
                    ZipPostalCode = model.ZipPostalCode,
                    PhoneNumber = model.PhoneNumber,
                    FaxNumber = model.FaxNumber,
                    CustomAttributes = model.CustomAttributes,
                    CreatedOnUtc = DateTime.Now,
                };

                return _BillingAddressRepository.Update(query, param);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<CustomerShoppingCartViewModel> GetShoppingCart(int CustomerID, int ShoppingcarttypeID)
        {
            string Query = @"select ShoppingCartItem.ProductId,Product.Name as ProductName,Product.Price,ShoppingCartItem.Quantity,
                            Product.ShortDescription as Description,Picture.PictureBinary 
                            from ShoppingCartItem 
                            join Product on ShoppingCartItem.ProductId = Product.Id
                            join Product_Picture_Mapping on Product.Id  = Product_Picture_Mapping.ProductId
                            join Picture on Picture.Id = Product_Picture_Mapping.PictureId
                            where ShoppingCartItem.CustomerId=@customerid and ShoppingCartItem.ShoppingCartTypeId=@shoppingcarttypeid";
            var param = new { customerid = CustomerID, shoppingcarttypeid = ShoppingcarttypeID };
            var result = _ShoppingCartItemRepository.Get<CustomerShoppingCartViewModel>(Query, param).ToList();
            result.ForEach(x =>
            {
                Product product = new Product();
                product.Id = x.ProductId;
                product.Name = x.ProductName;
                x.PictureModel = _productService.PrepareProductDetailsModel(product);
            });
            return result;
        }

        public string AddtoCart(int productId, int shoppingCartTypeId, int quantity, int CustomerID)
        {
            var cartType = (ShoppingCartTypeEnum)shoppingCartTypeId;

            string query = "select * from Product where Id = @Id and Deleted = 0";
            var param = new { Id = productId };
            var product = _productRepository.Get(query, param);
            if (product != null)
            {
                string Query = @"INSERT INTO [dbo].[ShoppingCartItem]([StoreId],[ShoppingCartTypeId],[CustomerId],[ProductId],[CustomerEnteredPrice],[Quantity],[CreatedOnUtc],[UpdatedOnUtc])
                               VALUES (@storeid,@shoppingcartid,@customerid,@productid,@CustomerEnteredPrice,@qty,@createddate,@updateddate)";
                var Parameters = new
                {
                    storeid = 1,
                    shoppingcartid = shoppingCartTypeId,
                    customerid = CustomerID,
                    productid = productId,
                    qty = quantity,
                    CustomerEnteredPrice = 0,
                    createddate = DateTime.Now,
                    updateddate = DateTime.Now
                };

                var AddCart = _ShoppingCartItemRepository.Add(Query, Parameters);
                return "The product has been added to your shopping cart";
            }
            return string.Empty;
        }

        public List<CustomerShoppingCartViewModel> UpdateShoppingCart(ShoppingCartItemViewModel Model)
        {
            string Query = string.Empty;
            if (Model.Removefromcart)
            {
                foreach (var item in Model.ItesList)
                {
                    Query = @"delete from ShoppingCartItem where CustomerId = @custid and ProductId = @productid and ShoppingCartTypeId=@shoppingcarttypeid";
                    var Param = new { custid = Model.CustomerId, productid = item.ProductId, shoppingcarttypeid = Model.ShoppingCartTypeId };
                    var Res = _ShoppingCartItemRepository.Delete(Query, Param);
                }
            }
            else
            {
                foreach (var item in Model.ItesList)
                {
                    Query = @"update ShoppingCartItem set Quantity = @qty where CustomerId = @custid and ProductId = @prodductid and ShoppingCartTypeId=@shoppingcarttypeid";
                    var Param = new { qty = item.Quantity, custid = Model.CustomerId, productid = item.ProductId, shoppingcarttypeid = Model.ShoppingCartTypeId };
                    var Res = _ShoppingCartItemRepository.Update(Query, Param);
                }
            }
            var UpadtedSchppingCart = GetShoppingCart(Model.CustomerId, Model.ShoppingCartTypeId);
            return UpadtedSchppingCart;
        }

    }
}
