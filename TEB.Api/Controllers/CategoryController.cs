using System.Web.Http;
using TEB.Core.Domain;
using TEB.Core.ViewModel;
using TEB.Service;

namespace TEB.Api.Controllers
{
    [AllowAnonymous]
    public class CategoryController : BaseApiController
    {
        public readonly ICategoryService _categoryService;
        // GET: Category
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public IHttpActionResult CategoryList(CategoryListModel model)
        {
            model.SearchCategoryName = "";
            model.SearchStoreId = 1;
            model.Page = 0;
            model.PageSize = int.MaxValue;
            model.showHidden = true;
            return RunInSafe(() =>
            {
                var data = _categoryService.GetAllCategories(model);
                tebResponse.Data = data.Result;
                tebResponse.IsSuccess = true;
                return Ok(tebResponse);
            });
        }

        public IHttpActionResult GetCategoryById(int Id)
        {
            return RunInSafe(() =>
            {
                var data = _categoryService.GetCategoryById(Id);
                tebResponse.Data = data;
                tebResponse.IsSuccess = true;
                return Ok(tebResponse);
            });
        }

        [HttpPost]
        public IHttpActionResult InsertCategory(Category model)
        {
            return RunInSafe(() =>
            {
                var data = _categoryService.InsertCategory(model);
                tebResponse.Data = data;
                tebResponse.IsSuccess = true;
                return Ok(tebResponse);
            });
        }

        [HttpPost]
        public IHttpActionResult UpdateCategory(Category model)
        {
            return RunInSafe(() =>
            {
                var data = _categoryService.UpdateCategory(model);
                tebResponse.Data = data;
                tebResponse.IsSuccess = true;
                return Ok(tebResponse);
            });
        }

        [HttpDelete]
        public IHttpActionResult DeleteCategory(int Id)
        {
            return RunInSafe(() =>
            {
                var data = _categoryService.DeleteCategory(Id);
                tebResponse.Data = data;
                tebResponse.IsSuccess = true;
                return Ok(tebResponse);
            });
        }

        [HttpGet]
        public IHttpActionResult GetAllCategoryAndProducts()
        {
            return RunInSafe(() =>
            {
                var data = _categoryService.GetAllCategoryAndProducts();
                tebResponse.Data = data;
                tebResponse.IsSuccess = true;
                return Ok(tebResponse);
            });
        }

        [HttpGet]
        public IHttpActionResult GetProductByCategoryID(int CategoryID,int Pageid,string Productsname="", string SortbyText = "")
        {
            return RunInSafe(() =>
            {
                var data = _categoryService.GetProductByCategoryID(CategoryID,Pageid, Productsname, SortbyText);
                tebResponse.Data = data;
                tebResponse.IsSuccess = true;
                return Ok(tebResponse);
            });
        }

        [HttpGet]
        public IHttpActionResult GetProductDetailsByID(int ID)
        {
            return RunInSafe(() =>
            {
                var data = _categoryService.GetProductDetails(ID);
                tebResponse.Data = data;
                tebResponse.IsSuccess = true;
                return Ok(tebResponse);
            });
        }
    }
}