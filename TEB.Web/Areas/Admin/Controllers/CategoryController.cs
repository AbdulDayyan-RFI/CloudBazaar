using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using TEB.Core.Common;
using TEB.Core.Domain;
using TEB.Core.Mapping;
using TEB.Core.ViewModel;
using TEB.Web.Controllers;

namespace TEB.Web.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        // GET: Admin/Category
        public ActionResult Category()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CategoryList(DataSourceRequest command, CategoryListModel model)
        {
            //if (!_permissionService.Authorize(StandardPermissionProvider.ManageCategories))
            //    return AccessDeniedKendoGridJson();

            CategoryViewModel categoryViewModel = new CategoryViewModel();
            model.Page = command.Page - 1;
            model.PageSize = command.PageSize;

            TEBApiResponse apiResponse = await Post<CategoryListModel>("/Category/CategoryList", model);
            if (apiResponse.IsSuccess)
            {
                List<Category> listCategories = JsonConvert.DeserializeObject<List<Category>>(Convert.ToString(apiResponse.Data));
                PagedList<Category> categories = new PagedList<Category>(listCategories, 0, 10);

                var gridModel = new DataSourceResult();
                gridModel.Data = listCategories.Select(x =>
                {
                    var categoryModel = CategoryMapping.ModelToView(x);
                    return categoryModel;
                });
                gridModel.Total = categories.TotalCount;
                return Json(gridModel);
            }
            return Json(0);
        }

        public ActionResult Create()
        {
            //if (!_permissionService.Authorize(StandardPermissionProvider.ManageCategories))
            //    return AccessDeniedView();

            var model = new CategoryViewModel();
            //locales
            //  AddLocales(_languageService, model.Locales);
            //templates
            //PrepareTemplatesModel(model);
            //categories
            PrepareAllCategoriesModel(model);
            ////discounts
            //PrepareDiscountModel(model, null, true);
            ////ACL
            //PrepareAclModel(model, null, false);
            ////Stores
            //PrepareStoresMappingModel(model, null, false);
            ////default values
            //model.PageSize = _catalogSettings.DefaultCategoryPageSize;
            //model.PageSizeOptions = _catalogSettings.DefaultCategoryPageSizeOptions;
            model.Published = true;
            model.IncludeInTopMenu = true;
            model.AllowCustomersToSelectPageSize = true;

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CategoryViewModel model, string savecontinue)
        {
            if (ModelState.IsValid)
            {
                bool continueEditing = false;
                if (!String.IsNullOrWhiteSpace(savecontinue))
                    continueEditing = true;
                Category objcategory = new Core.Domain.Category();
                objcategory = CategoryMapping.ViewToModel(model);
                TEBApiResponse apiResponse = await Post<Category>("/Category/InsertCategory", objcategory);
                if (apiResponse.IsSuccess)
                {
                    if (continueEditing)
                    {
                        //selected tab
                        //SaveSelectedTabName();
                        int categoryid = JsonConvert.DeserializeObject<int>(Convert.ToString(apiResponse.Data));
                        return RedirectToAction("Edit", new { id = categoryid });
                    }
                    return RedirectToAction("Category");
                }
            }
            return View(model);

        }

        public async Task<ActionResult> Edit(int id)
        {
            CategoryViewModel model = new CategoryViewModel();
            TEBApiResponse apiResponse = await Get("/Category/GetCategoryById?Id=" + id);
            if (apiResponse.IsSuccess)
            {
                Category product = JsonConvert.DeserializeObject<Category>(Convert.ToString(apiResponse.Data));
                model = CategoryMapping.ModelToView(product);

            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(CategoryViewModel model, string savecontinue)
        {
            if (ModelState.IsValid)
            {
                bool continueEditing = false;
                if (!String.IsNullOrWhiteSpace(savecontinue))
                    continueEditing = true;
                Category category = new Category();
                category = CategoryMapping.ViewToModel(model);
                TEBApiResponse apiResponse = await Post<Category>("/Category/UpdateCategory", category);
                if (apiResponse.IsSuccess)
                {
                    if (continueEditing)
                    {
                        //selected tab
                        //SaveSelectedTabName();
                        int categoryid = JsonConvert.DeserializeObject<int>(Convert.ToString(apiResponse.Data));
                        return RedirectToAction("Edit", new { id = categoryid });
                    }
                    return RedirectToAction("Category");
                }
            }
            return View(model);
        }

        public async Task<ActionResult> Delete(int id)
        {
            TEBApiResponse apiResponse = await Delete("/Category/DeleteCategory?Id=" + id);
            return RedirectToAction("Category");
        }

        [NonAction]
        protected virtual void PrepareAllCategoriesModel(CategoryViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            model.AvailableCategories.Add(new SelectListItem
            {
                Text = "None",
                Value = "0"
            });
            List<Category> categorieslist = new List<Category>();
            List<SelectListItem> categories = categorieslist.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
            foreach (var c in categories)
                model.AvailableCategories.Add(c);
        }


    }
}