using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using TEB.Service.Orders;

namespace TEB.Api.Controllers
{
    public class OrderController : BaseApiController
    {

        private readonly IOrderService _IOrderSrvice;

        public OrderController(IOrderService _IOrderSrvice)
        {
            this._IOrderSrvice = _IOrderSrvice;
        }

        public IHttpActionResult GetCustomerOrders(int CustomerID)
        {
            return RunInSafe(()=>
            {
                var res = _IOrderSrvice.GetOrders(CustomerID);
                tebResponse.Data = res;
                tebResponse.IsSuccess = true;
                return Ok(tebResponse);
            });
        }

        public IHttpActionResult GetSingleOrder(int OrderID)
        {
            return RunInSafe(() =>
            {
                var res = _IOrderSrvice.GetSingleOrders(OrderID);
                tebResponse.Data = res;
                tebResponse.IsSuccess = true;
                return Ok(tebResponse);
            });
        }

        [System.Web.Http.HttpGet]
        public IHttpActionResult placeOrder(int CustomerID)
        {
            return RunInSafe(() =>
            {
                _IOrderSrvice.PlaceOrder(CustomerID);
                tebResponse.Data = 0;
                tebResponse.IsSuccess = true;
                return Ok(tebResponse);
            });
        }

    }
}