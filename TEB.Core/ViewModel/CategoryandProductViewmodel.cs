using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEB.Core.ViewModel
{
    public class CategoryandProductViewmodel
    {
        public CategoryandProductViewmodel()
        {
            ChildCategory = new List<ChildCategory>();
            ProductList = new List<CategoriesProductsViewModel>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int ParentCategoryId { get; set; }
        
        public List<ChildCategory> ChildCategory { get; set; }
        public List<CategoriesProductsViewModel> ProductList { get; set; }
    }

    public class CategoriesProductsViewModel
    {
        public int CategoryId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public byte[] PicBinary { get; set; }
        public string ImageURL { get; set; }
        public double Price { get; set; }
    }

    public class ChildCategory
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
