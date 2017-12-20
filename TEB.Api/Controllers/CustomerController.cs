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
    [AllowAnonymous]
    public class CustomerController : BaseApiController
    {
        public readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public IHttpActionResult GetAllCustomersList()
        {
            return RunInSafe(() =>
            {
                var data = _customerService.GetAllCustomersList();
                tebResponse.Data = data;
                tebResponse.IsSuccess = true;
                return Ok(tebResponse);
            });
        }

        [HttpPost]
        public IHttpActionResult InsertCustomer(Customer customer)
        {
            return RunInSafe(() =>
            {
                var data = _customerService.InsertCustomer(customer);
                tebResponse.Data = data;
                tebResponse.IsSuccess = true;
                return Ok(tebResponse);
            });
        }

        public IHttpActionResult GetCustomerById(int Id)
        {
            return RunInSafe(() =>
            {
                var data = _customerService.GetCustomerById(Id);
                tebResponse.Data = data;
                tebResponse.IsSuccess = true;
                return Ok(tebResponse);
            });
        }

        [HttpPost]
        public IHttpActionResult UpdateCustomer(CustomerViewModel model)
        {
            return RunInSafe(() =>
            {
                var data = _customerService.UpdateCustomer(model);
                tebResponse.Data = data;
                tebResponse.IsSuccess = true;
                return Ok(tebResponse);
            });
        }

        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int Id)
        {
            return RunInSafe(() =>
            {
                var data = _customerService.DeleteCustomer(Id);
                tebResponse.Data = data;
                tebResponse.IsSuccess = true;
                return Ok(tebResponse);
            });
        }

        public IHttpActionResult GetCustomerByUsername(string userName)
        {
            return RunInSafe(() =>
            {
                var data = _customerService.GetCustomerByUsername(userName);
                tebResponse.Data = data;
                tebResponse.IsSuccess = true;
                return Ok(tebResponse);
            });
        }

        public IHttpActionResult GetCustomerByEmail(string email)
        {
            return RunInSafe(() =>
            {
                var data = _customerService.GetCustomerByEmail(email);
                tebResponse.Data = data;
                tebResponse.IsSuccess = true;
                return Ok(tebResponse);
            });
        }

    }
}
