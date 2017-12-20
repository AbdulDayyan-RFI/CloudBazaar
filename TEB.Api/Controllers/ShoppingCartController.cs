using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TEB.Core.Domain;
using TEB.Core.ViewModel;
using TEB.Service;

namespace TEB.Api.Controllers
{
    public class ShoppingCartController : BaseApiController
    {
        public readonly IShopingCartService _IShopingCartService;

        public ShoppingCartController(IShopingCartService IShopingCartService)
        {
            _IShopingCartService = IShopingCartService;
        }

        [HttpPost]
        public IHttpActionResult InsertBillingAdress(Address model)
        {
            return RunInSafe(() =>
            {
                var data = _IShopingCartService.InsertBillingAddress(model);
                tebResponse.Data = data;
                tebResponse.IsSuccess = true;
                return Ok(tebResponse);
            });
        }

        [HttpGet]
        public IHttpActionResult GetShoppingCart(int CustomerID, int ShoppingcarttypeID)
        {
            return RunInSafe(() =>
            {
                var data = _IShopingCartService.GetShoppingCart(CustomerID, ShoppingcarttypeID);
                tebResponse.Data = data;
                tebResponse.IsSuccess = true;
                return Ok(tebResponse);
            });
        }

        [HttpPost]
        public IHttpActionResult AddToCart(int productId, int shoppingCartTypeId, int quantity, int CustomerID)
        {
            return RunInSafe(() =>
            {
                var data = _IShopingCartService.AddtoCart(productId, shoppingCartTypeId, quantity, CustomerID);
                tebResponse.Data = data;
                tebResponse.IsSuccess = true;
                return Ok(tebResponse);
            });
        }

        [HttpGet]
        public IHttpActionResult UpdateShoppingCartApp(string productId, int shoppingCartTypeId, int quantity, int CustomerID, bool Removefromcart)
        {
            List<ShoppingCartItemViewModel> productlist = new List<ShoppingCartItemViewModel>();
            var list = productId.Split(',').Where(x => x != "").Select(x => Convert.ToInt32(x)).ToList();
            foreach (var item in list)
            {
                ShoppingCartItemViewModel model = new ShoppingCartItemViewModel();
                model.ProductId = item;
                productlist.Add(model);
            }
            ShoppingCartItemViewModel Model = new ShoppingCartItemViewModel
            {
                ShoppingCartTypeId = shoppingCartTypeId,
                Quantity = quantity,
                CustomerId = CustomerID,
                ItesList = productlist,
                Removefromcart = Removefromcart
            };
            return RunInSafe(() =>
            {
                var data = _IShopingCartService.UpdateShoppingCart(Model);
                tebResponse.Data = data;
                tebResponse.IsSuccess = true;
                return Ok(tebResponse);
            });
        }
    }
}
