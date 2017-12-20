using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEB.Core.Domain;

namespace TEB.Service
{
    public interface IProduct_SpecificationAttribute_MappingService
    {
        Task<IEnumerable<Product_SpecificationAttribute_Mapping>> GetProductSpecificationAttributes(int productId = 0,
           int specificationAttributeOptionId = 0, bool? allowFiltering = null, bool? showOnProductPage = null);
    }
}
