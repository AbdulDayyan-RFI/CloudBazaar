using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEB.Core.Common;
using TEB.Core.Domain;
using TEB.Core.ViewModel;

namespace TEB.Service
{
    public interface IProductService
    {
        object InsertProduct(Product model);
        Product GetProductById(int Id);
        int UpdateProduct(Product model);
        int DeleteProduct(int Id);
        Task<IEnumerable<Product>> SearchProducts(SearchProductModel model);
        ProductsViewModel GetProductByName(string ProductName);
    }
}
