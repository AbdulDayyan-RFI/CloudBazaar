using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEB.Core.ViewModel;
using TEB.Core.Enumerations;
using TEB.Core.Domain;
using TEB.Data;

namespace TEB.Service.Orders
{
    public interface IOrderService
    {
        List<CustomerOrderViewModel> GetOrders(int CustomerID);
        CustomerOrderViewModel GetSingleOrders(int OrderID);
        void PlaceOrder(int Customerid);
    }
}
