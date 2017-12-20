using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TEB.Core.Common;
using TEB.Core.Domain;
using TEB.Core.ViewModel;
using TEB.Service;

namespace TEB.Api.Controllers
{
    public class UserController : BaseApiController
    {
        private readonly IUserService _userService;


        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IHttpActionResult GetUserDetails(string Email, string Password)
        {
            return RunInSafe(() =>
            {
                var Res = _userService.GetUserDetails(Email, Password);
                tebResponse.Data = Res;
                tebResponse.IsSuccess = true;
                return Ok(tebResponse);
            });
        }

        [HttpPost]
        public IHttpActionResult PostCustomer(CustomerViewModel Model)
        {
            return RunInSafe(() =>
            {
                var Cust = _userService.RegisterUser(Model);
                tebResponse.Data = Cust;
                tebResponse.IsSuccess = true;
                return Ok(tebResponse);
            });
        }

        [HttpPost]
        public IHttpActionResult UpdateCustomer(CustomerViewModel model)
        {
            return RunInSafe(() =>
            {
                var data = _userService.UpdateCustomer(model);
                tebResponse.Data = data;
                tebResponse.IsSuccess = true;
                return Ok(tebResponse);
            });
        }

        [HttpGet]
        public IHttpActionResult ForgetPassword(string Email)
        {
            return RunInSafe(() =>
            {
                var Data = _userService.ForgetPassword(Email);
                tebResponse.Data = Data;
                tebResponse.IsSuccess = true;
                return Ok(tebResponse);
            });
        }

        [HttpGet]
        public IHttpActionResult GetCustomerDetails(string UserName)
        {
            return RunInSafe(() =>
            {
                var Data = _userService.GetAccountDetails(UserName);
                tebResponse.Data = Data;
                tebResponse.IsSuccess = true;
                return Ok(tebResponse);
            });
        }

        [HttpGet]
        public IHttpActionResult GetShippingOrBillingAddress(int CustomerID)
        {
            return RunInSafe(() =>
            {
                var Data = _userService.GetShippingOrBillingAddress(CustomerID);
                tebResponse.Data = Data;
                tebResponse.IsSuccess = true;
                return Ok(tebResponse);
            });
        }

        [HttpGet]
        public IHttpActionResult GetCustomerAddress(int CustomerID)
        {
            return RunInSafe(() =>
            {
                var Data = _userService.GetAddress(CustomerID);
                tebResponse.Data = Data;    
                tebResponse.IsSuccess = true;
                return Ok(tebResponse);
            });
        }

        [HttpGet]
        public IHttpActionResult GetCustomerAddressByID(int AddressID)
        {
            return RunInSafe(() =>
            {
                var Data = _userService.GetSingleAddress(AddressID);
                tebResponse.Data = Data;
                tebResponse.IsSuccess = true;
                return Ok(tebResponse);
            });
        }

        [HttpPost]
        public IHttpActionResult AddCustomerAddress(AddressViewModel Model)
        {
            return RunInSafe(() =>
            {
                var Data = _userService.AddAddress(Model);
                tebResponse.Data = Data;
                tebResponse.IsSuccess = true;
                return Ok(tebResponse);
            });
        }

        [HttpPut]
        public IHttpActionResult UpdateCustomerAddress(AddressViewModel Model)
        {
            return RunInSafe(() =>
            {
                var Data = _userService.UpdateAddress(Model);
                tebResponse.Data = Data;
                tebResponse.IsSuccess = true;
                return Ok(tebResponse);
            });
        }

        [HttpDelete]
        public IHttpActionResult DeleteCustomerAddress(int addressId)
        {
            return RunInSafe(() =>
            {
                var Data = _userService.DeleteAddress(addressId);
                tebResponse.Data = Data;
                tebResponse.IsSuccess = true;
                return Ok(tebResponse);
            });
        }

    }
}
