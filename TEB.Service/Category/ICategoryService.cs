using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEB.Core;
using TEB.Core.Common;
using TEB.Core.Domain;
using TEB.Core.ViewModel;

namespace TEB.Service
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategories(CategoryListModel model);
        Category GetCategoryById(int categoryId);
        object InsertCategory(Category model);
        int UpdateCategory(Category model);
        int DeleteCategory(int Id);
        IEnumerable<CategoryandProductViewmodel> GetAllCategoryAndProducts();
        IEnumerable<ProductsViewModel> GetProductByCategoryID(int CategoryID, int PageId);
        ProductsViewModel GetProductDetails(int ID);
    }
}
