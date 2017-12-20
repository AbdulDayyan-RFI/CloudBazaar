using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEB.Data;
using TEB.Core.Domain;

namespace TEB.Service
{
    public class Product_SpecificationAttribute_MappingService : IProduct_SpecificationAttribute_MappingService
    {
        private readonly IGenericRepository<Product_SpecificationAttribute_Mapping> _product_SpecificationAttribute_Mapping;
        public Product_SpecificationAttribute_MappingService(IGenericRepository<Product_SpecificationAttribute_Mapping> product_SpecificationAttribute_Mapping)
        {
            _product_SpecificationAttribute_Mapping = product_SpecificationAttribute_Mapping;
        }

        public Task<IEnumerable<Product_SpecificationAttribute_Mapping>> GetProductSpecificationAttributes(int productId = 0,
           int specificationAttributeOptionId = 0, bool? allowFiltering = null, bool? showOnProductPage = null)
        {
            var query = "select * from Product_SpecificationAttribute_Mapping where ProductId=@ProductId order by DisplayOrder, Id";
            var param = new { productId = productId };
            var list = _product_SpecificationAttribute_Mapping.SqlQuery(query, param);
            return list;
        }
    }
}
