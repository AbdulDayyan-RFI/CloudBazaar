using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEB.Core.ViewModel
{
    public class ProductsViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public byte[] PicBinary { get; set; }
        public double Price { get; set; }
        public string FullDescription { get; set; }
        public string ShortDescription { get; set; }
        public List<byte[]> Pictures { get; set; }
        public List<ProductsViewModel> RelateProducts { get; set; }
    }
}
