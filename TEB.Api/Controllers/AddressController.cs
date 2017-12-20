using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TEB.Service;

namespace TEB.Api.Controllers
{
    [AllowAnonymous]
    public class AddressController : BaseApiController
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }
        public IHttpActionResult GetCustomerAddressesById(int Id)
        {
            return RunInSafe(() =>
            {
                var data = _addressService.GetAllAddressByCustomerId(Id);
                tebResponse.Data = data;
                tebResponse.IsSuccess = true;
                return Ok(tebResponse);
            });
        }
    }
}
