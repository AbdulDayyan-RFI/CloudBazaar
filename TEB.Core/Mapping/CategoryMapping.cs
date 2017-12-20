using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEB.Core.Domain;
using TEB.Core.ViewModel;

namespace TEB.Core.Mapping
{
    public static class CategoryMapping
    {
        public static Category ViewToModel(CategoryViewModel model)
        {
            Category category = new Category
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
                CreatedOnUtc = model.CreatedOnUtc,
                UpdatedOnUtc = model.UpdatedOnUtc

            };
            return category;
        }
        public static CategoryViewModel ModelToView(Category model)
        {
            CategoryViewModel categoryViewModel = new CategoryViewModel
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
                CreatedOnUtc = model.CreatedOnUtc,
                UpdatedOnUtc = model.UpdatedOnUtc
            };
            return categoryViewModel;
        }

    }
}
