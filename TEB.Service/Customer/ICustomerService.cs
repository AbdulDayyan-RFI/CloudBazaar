using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEB.Core.Domain;
using TEB.Core.ViewModel;

namespace TEB.Service
{
    public interface ICustomerService
    {
        object InsertCustomer(Customer model);
        Customer GetCustomerById(int Id);
        int UpdateCustomer(CustomerViewModel model);
        int DeleteCustomer(int Id);
        Task<IEnumerable<Customer>> GetAllCustomersList();
        Customer GetCustomerByUsername(string userName);
        Customer GetCustomerByEmail(string email);
    }
}
