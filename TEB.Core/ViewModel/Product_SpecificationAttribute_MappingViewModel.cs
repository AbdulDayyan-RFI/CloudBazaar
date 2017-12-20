using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TEB.Core.ViewModel
{
    public class Product_SpecificationAttribute_MappingViewModel
    {
        public int Id { get; set; }

        public int AttributeTypeId { get; set; }

        [AllowHtml]
        public string AttributeTypeName { get; set; }

        public int AttributeId { get; set; }

        [AllowHtml]
        public string AttributeName { get; set; }

        [AllowHtml]
        public string ValueRaw { get; set; }

        public bool AllowFiltering { get; set; }

        public bool ShowOnProductPage { get; set; }

        public int DisplayOrder { get; set; }

        public int SpecificationAttributeOptionId { get; set; }
    }
}
