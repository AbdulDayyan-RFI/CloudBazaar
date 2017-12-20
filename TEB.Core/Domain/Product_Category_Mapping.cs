using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEB.Core.Domain
{
    public class Product_Category_Mapping
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public bool IsFeaturedProduct { get; set; }
        public int DisplayOrder { get; set; }
    }
}
