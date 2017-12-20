using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TEB.Service.CheckOut;

namespace TEB.Api.Controllers
{
    public class CheckOutController : BaseApiController
    {
        private readonly ICheckOutService _ICheckOutService;
        public CheckOutController(ICheckOutService ICheckOutService)
        {
            _ICheckOutService = ICheckOutService;
        }

        public IHttpActionResult CheckOut()
        {
            return RunInSafe(() =>
            {
                tebResponse.Data = 0;
                tebResponse.IsSuccess = true;
                return Ok(tebResponse);
            });
        }
    }
}
