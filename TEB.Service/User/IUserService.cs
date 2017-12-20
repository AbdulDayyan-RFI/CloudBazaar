using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEB.Core.Common;
using TEB.Core.Domain;
using TEB.Core.ViewModel;

namespace TEB.Service
{
    public interface IUserService
    {
        Customer GetUserDetails(string Email, string password);
        Customer RegisterUser(CustomerViewModel Model);
        int UpdateCustomer(CustomerViewModel model);
        string ForgetPassword(string Email);
        Customer GetAccountDetails(string UserName);
        List<AddressViewModel> GetShippingOrBillingAddress(int CustomerID);
        List<AddressViewModel> GetAddress(int CustomerID);
        AddressViewModel GetSingleAddress(int AddressID);
        object AddAddress(AddressViewModel Model);
        object UpdateAddress(AddressViewModel Model);
        object DeleteAddress(int AddressID);
    }
}
