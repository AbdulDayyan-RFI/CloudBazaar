using System.Web.Http;
using TEB.Service;

namespace TEB.Api.Controllers
{
    [System.Web.Http.AllowAnonymous]
    public class HomeController : BaseApiController
    {
        public readonly ICategoryService _categoryService;
        
        public HomeController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IHttpActionResult Hello()
        {
            return RunInSafe(() =>
            {
                var data = "HelloWorld";
                tebResponse.Data = data;
                tebResponse.IsSuccess = true;
                return Ok(tebResponse);
            });
        }
    }
}
