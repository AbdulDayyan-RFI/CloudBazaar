using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TEB.Core.Common;
using TEB.Core.Domain;
using TEB.Core.Mapping;
using TEB.Core.ViewModel;
using TEB.Data;

namespace TEB.Service
{
    public class CategoryService : ICategoryService
    {

        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IGenericRepository<Product> _productRepository;
        public readonly IProductService _productService;

        public CategoryService(IProductService productService, IGenericRepository<Category> categoryRepository, IGenericRepository<Product> productRepository)
        {
            _productService = productService;
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }

        public Task<IEnumerable<Category>> GetAllCategories(CategoryListModel model)
        {

            var query = "CategoryLoadAllPaged";
            var param = new DynamicParameters();
            param.Add("ShowHidden", model.showHidden, DbType.Boolean);
            param.Add("Name", model.SearchCategoryName ?? string.Empty, DbType.String);
            param.Add("StoreId", model.SearchStoreId, DbType.Int32);
            param.Add("CustomerRoleIds", null, DbType.String);
            param.Add("PageIndex", model.Page, DbType.Int32);
            param.Add("PageSize", model.PageSize, DbType.Int32);
            param.Add("TotalRecords", ParameterDirection.Output, DbType.Int32);
            var list = _categoryRepository.SqlStoredProcedure(query, param);
            var sublist = list.Result.Select(x => new Category
            {
                Id = x.Id,
                Name = GetFormattedBreadCrumb(x),
                Published = x.Published,
                DisplayOrder = x.DisplayOrder,

            }).ToList();

            return Task.FromResult<IEnumerable<Category>>(sublist);
        }

        public string GetFormattedBreadCrumb(Category category, string separator = ">>")
        {
            string result = string.Empty;
            var breadcrumb = GetCategoryBreadCrumb(category, true);
            for (int i = 0; i <= breadcrumb.Count - 1; i++)
            {
                var categoryName = breadcrumb[i].Name;
                result = String.IsNullOrEmpty(result)
                    ? categoryName
                    : string.Format("{0} {1} {2}", result, separator, categoryName);
            }
            return result;
        }
        public List<Category> GetCategoryBreadCrumb(Category category, bool showHidden = false)
        {
            if (category == null)
                throw new ArgumentNullException("category");

            var result = new List<Category>();
            var alreadyProcessedCategoryIds = new List<int>();
            while (category != null &&
                !category.Deleted &&
                (showHidden || category.Published) &&
                !alreadyProcessedCategoryIds.Contains(category.Id))
            {
                result.Add(category);
                alreadyProcessedCategoryIds.Add(category.Id);
                category = GetCategoryById(category.ParentCategoryId);
            }
            result.Reverse();
            return result;
        }

        public Category GetCategoryById(int categoryId)
        {
            var query = "select * from category where Id = @id";
            var param = new { Id = categoryId };
            var category = _categoryRepository.Get(query, param);
            return category;

        }

        public object InsertCategory(Category model)
        {
            var query = @"INSERT [dbo].[Category] ([Name] ,[Description] ,[CategoryTemplateId] ,
                                      [MetaKeywords] ,[MetaDescription] ,[MetaTitle] ,[ParentCategoryId] ,
                                      [PictureId] ,[PageSize] ,[AllowCustomersToSelectPageSize] ,[PageSizeOptions] ,
                                      [PriceRanges] ,[ShowOnHomePage] ,[IncludeInTopMenu] ,[SubjectToAcl] ,
                                      [LimitedToStores] ,[Published] ,[Deleted] ,[DisplayOrder] ,[CreatedOnUtc] ,
                                      [UpdatedOnUtc]) VALUES( @Name, @Description, @CategoryTemplateId, 
                                      @MetaKeywords, @MetaDescription, @MetaTitle, @ParentCategoryId, 
                                      @PictureId, @PageSize, @AllowCustomersToSelectPageSize, @PageSizeOptions, 
                                      @PriceRanges, @ShowOnHomePage, @IncludeInTopMenu, @SubjectToAcl,
                                      @LimitedToStores, @Published, @Deleted, @DisplayOrder, GETUTCDATE(), 
                                      GETUTCDATE()); SELECT SCOPE_IDENTITY()";
            var param = new
            {
                Name = model.Name,
                Description = model.Description,
                CategoryTemplateId = model.CategoryTemplateId,
                MetaKeywords = model.MetaKeywords,
                MetaDescription = model.MetaDescription,
                MetaTitle = model.MetaTitle,
                ParentCategoryId = model.ParentCategoryId,
                PictureId = model.PictureId,
                PageSize = model.PageSize,
                AllowCustomersToSelectPageSize = model.AllowCustomersToSelectPageSize,
                PageSizeOptions = model.PageSizeOptions,
                PriceRanges = model.PriceRanges,
                ShowOnHomePage = model.ShowOnHomePage,
                IncludeInTopMenu = model.IncludeInTopMenu,
                SubjectToAcl = model.SubjectToAcl,
                LimitedToStores = model.LimitedToStores,
                Published = model.Published,
                Deleted = model.Deleted,
                DisplayOrder = model.DisplayOrder,
            };
            return _categoryRepository.Update(query, param);

        }

        public int UpdateCategory(Category model)
        {
            var query = @"UPDATE [dbo].[Category] SET Name = @Name, Description = @Description, CategoryTemplateId = @CategoryTemplateId, 
      MetaKeywords = @MetaKeywords, MetaDescription = @MetaDescription,MetaTitle = @MetaTitle, ParentCategoryId = @ParentCategoryId, 
      PictureId = @PictureId, PageSize = @PageSize, AllowCustomersToSelectPageSize = @AllowCustomersToSelectPageSize, 
      PageSizeOptions = @PageSizeOptions, PriceRanges = @PriceRanges, ShowOnHomePage = @ShowOnHomePage, IncludeInTopMenu = @IncludeInTopMenu, 
      SubjectToAcl = @SubjectToAcl, LimitedToStores = @LimitedToStores, Published = @Published, Deleted = @Deleted, DisplayOrder = @DisplayOrder, 
      UpdatedOnUtc = GETUTCDATE() WHERE Id = @Id; SELECT SCOPE_IDENTITY();";

            var param = new
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                CategoryTemplateId = model.CategoryTemplateId,
                MetaKeywords = model.MetaKeywords,
                MetaDescription = model.MetaDescription,
                MetaTitle = model.MetaTitle,
                ParentCategoryId = model.ParentCategoryId,
                PictureId = model.PictureId,
                PageSize = model.PageSize,
                AllowCustomersToSelectPageSize = model.AllowCustomersToSelectPageSize,
                PageSizeOptions = model.PageSizeOptions,
                PriceRanges = model.PriceRanges,
                ShowOnHomePage = model.ShowOnHomePage,
                IncludeInTopMenu = model.IncludeInTopMenu,
                SubjectToAcl = model.SubjectToAcl,
                LimitedToStores = model.LimitedToStores,
                Published = model.Published,
                Deleted = model.Deleted,
                DisplayOrder = model.DisplayOrder,
            };

            _categoryRepository.Update(query, param);
            return model.Id;
        }

        public int DeleteCategory(int Id)
        {
            var query = @"UPDATE [dbo].[Category] SET Deleted=1,UpdatedOnUtc=GETUTCDATE() WHERE Id = @Id";
            var param = new { Id = Id };
            return _categoryRepository.Delete(query, param);
        }

        public IEnumerable<CategoryandProductViewmodel> GetAllCategoryAndProducts()
        {
            List<CategoryandProductViewmodel> model = new List<CategoryandProductViewmodel>();

            var category = @"select Category.Id as CategoryId, Category.Name as CategoryName,Category.ParentCategoryId as ParentCategoryId 
                            from Category
                            where Category.Deleted = 0 and Category.ParentCategoryId = 0";
            model = _categoryRepository.Get<CategoryandProductViewmodel>(category, null).ToList();

            string parentids = string.Join(",", model.Select(x => x.CategoryId).ToList());

            var childcategory = @"select Category.Id as CategoryId, Category.Name as CategoryName,Category.ParentCategoryId as ParentCategoryId 
                            from Category
                            where Category.Deleted = 0 and Category.ParentCategoryId in (" + parentids + ")";
            var param = new { parentids = model.Select(x => x.CategoryId).ToList() };
            var childcategorylist = _categoryRepository.Get<CategoryandProductViewmodel>(childcategory, null).ToList();

            foreach (var parent in model)
            {
                var childcategories = childcategorylist.Where(x => x.ParentCategoryId == parent.CategoryId).Select(x => new ChildCategory
                {
                    CategoryId = x.CategoryId,
                    CategoryName = x.CategoryName
                }).ToList();
                parent.ChildCategory = childcategories;
            }

            var categoryproducts = @"select distinct Product.Id as ProductId,Product.Name as ProductName,Product.Price as Price,Picture.PictureBinary as PicBinary,
                        Product_Category_Mapping.CategoryId as CategoryId 
                        from Product_Category_Mapping 
                        left outer join Product on Product.Id = Product_Category_Mapping.ProductId 
                        join Product_Picture_Mapping on Product_Picture_Mapping.ProductId  = Product.Id
                        left outer join Picture on Picture.Id = Product_Picture_Mapping.PictureId
                        where Product.Deleted = 0";
            var allCatogeryandProductsList = _categoryRepository.Get<CategoriesProductsViewModel>(categoryproducts, null).ToList();

            foreach (var item in model)
            {
                var products = allCatogeryandProductsList.Where(x => x.CategoryId == item.CategoryId).GroupBy(x => x.ProductId).Select(x => new CategoriesProductsViewModel
                {
                    ProductId = x.FirstOrDefault().ProductId,
                    ProductName = x.FirstOrDefault().ProductName,
                    PicBinary = x.FirstOrDefault().PicBinary,
                    Price = x.FirstOrDefault().Price
                }).ToList();
                if (products.Count > 0)
                {
                    item.ProductList = products;
                }
            }
            return model;
        }

        public IEnumerable<ProductsViewModel> GetProductByCategoryID(int CategoryID, int PageID, string Productsname = "", string SortbyText = "")
        {
            List<ProductsViewModel> ProductList = new List<ProductsViewModel>();

            int skip = 0;
            int take = 10;
            if (PageID != 1)
            {
                skip = take * (PageID - 1);
            }
            string query = @"";
            if (Productsname == "")
            {
                query = @"select p.Id as ProductId,p.Name as ProductName,p.Price,pic.PictureBinary as PicBinary from Product as p left outer join Product_Category_Mapping  as pc on p.Id = pc.ProductId 
                            left outer join Product_Picture_Mapping as ppm on p.id = ppm.ProductId left outer join Picture as pic on pic.Id = ppm.PictureId  where p.Published = 1 and p.Deleted = 0 and pc.CategoryId = @categoryid 
                            order by p.Id desc OFFSET (@skip) ROWS FETCH NEXT (@take) ROWS ONLY";

            }
            else
            {
                query = @"select p.Id as ProductId,p.Name as ProductName,p.Price,pic.PictureBinary as PicBinary from Product as p left outer join Product_Category_Mapping  as pc on p.Id = pc.ProductId 
                            left outer join Product_Picture_Mapping as ppm on p.id = ppm.ProductId left outer join Picture as pic on pic.Id = ppm.PictureId  where p.Published = 1 and p.Deleted = 0 and p.Name like '%@name%'
                            order by p.Id desc OFFSET (@skip) ROWS FETCH NEXT (@take) ROWS ONLY";
            }
            if (SortbyText != "")
            {
                switch (SortbyText)
                {
                    case "A to Z":
                        query = query.Replace("order by p.Id desc", "order by p.Name");
                        break;

                    case "Z to A":
                        query = query.Replace("order by p.Id desc", "order by p.Name desc");
                        break;

                    case "Low to High":
                        query = query.Replace("order by p.Id desc", "order by p.Price");
                        break;

                    case "High to Low":
                        query = query.Replace("order by p.Id desc", "order by p.Price desc");
                        break;

                    default:
                        break;
                }
            }
            var param = new
            {
                categoryid = CategoryID,
                name = Productsname,
                skip = skip,
                take = take
            };
            var products = _productRepository.Get<ProductsViewModel>(query, param).ToList();
            products.ForEach(x => {
            Product product = new Product();
                product.Id = x.ProductId;
                product.Name = x.ProductName;
                x.PictureModel = _productService.PrepareProductDetailsModel(product);

            });
            return products;
        }

        public ProductsViewModel GetProductDetails(int ID)
        {
            ProductsViewModel Model = new ProductsViewModel();
            string Query = @"select id as ProductId,Name as ProductName,Price,FullDescription,ShortDescription from Product where Product.Id = @id";
            var param = new { id = ID };
            Model = _productRepository.Get<ProductsViewModel>(Query, param).FirstOrDefault();


            string ImageQuery = @"select Picture.PictureBinary from Picture join Product_Picture_Mapping on Picture.Id = Product_Picture_Mapping.PictureId 
                                where Product_Picture_Mapping.ProductId = @id";
            var Params = new { id = ID };
            Model.Pictures = _productRepository.Get<byte[]>(ImageQuery, Params).ToList();

            string RelatedProductsQuery = @"select ProductId,ProductName,Price,Picture.PictureBinary as PicBinary from (
                                            select Product.id as ProductId,Product.Name as ProductName,Product.Price,max(Picture.Id) as PictureId
                                            from RelatedProduct
                                            join Product on RelatedProduct.ProductId2 = Product.Id 
                                            join Product_Picture_Mapping on Product.Id = Product_Picture_Mapping.ProductId
                                            join Picture on Picture.Id = Product_Picture_Mapping.PictureId
                                            where RelatedProduct.ProductId1 = @productid1
                                            group by Product.id,Product.Name,Product.Price  
                                            )a
                                            join Picture on Picture.Id = PictureId";
            var Parameters = new { productid1 = ID };
            Model.RelateProducts = _productRepository.Get<ProductsViewModel>(RelatedProductsQuery, Parameters).ToList();
            return Model;
        }

    }
}
