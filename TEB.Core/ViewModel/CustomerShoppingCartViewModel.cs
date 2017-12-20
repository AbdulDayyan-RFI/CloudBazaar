using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEB.Core.ViewModel
{
    public class CustomerShoppingCartViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public byte[] PictureBinary { get; set; }
    }
}
