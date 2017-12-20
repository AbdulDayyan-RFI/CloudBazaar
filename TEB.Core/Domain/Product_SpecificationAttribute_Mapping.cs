using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEB.Core.Domain
{
    public class Product_SpecificationAttribute_Mapping
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int AttributeTypeId { get; set; }
        public int SpecificationAttributeOptionId { get; set; }
        public string CustomValue { get; set; }
        public bool AllowFiltering { get; set; }
        public bool ShowOnProductPage { get; set; }
        public int DisplayOrder { get; set; }
    }
}
