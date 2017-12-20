using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEB.Core.ViewModel
{
    public class CustomerOrderViewModel
    {
        public int OrderID { get; set; }
        public string OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public double OrderPrice { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string ShipmentMethod { get; set; }
        public string ShipmentStatus { get; set; }
        public List<CustomerShoppingCartViewModel> ProductsList { get; set; }
        public AddressViewModel BillingAddress { get; set; }
        public AddressViewModel ShippingAddress { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }

    }
}
