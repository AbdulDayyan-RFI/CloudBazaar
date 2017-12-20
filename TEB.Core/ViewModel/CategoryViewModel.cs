using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TEB.Core.ViewModel
{
    public class CategoryViewModel
    {
        public CategoryViewModel()
        {
            AvailableCategoryTemplates= new List<SelectListItem>();
            AvailableCategories = new List<SelectListItem>();
            SelectedDiscountIds = new List<int>();
            AvailableDiscounts = new List<SelectListItem>();
            SelectedCustomerRoleIds = new List<int>();
            AvailableCustomerRoles = new List<SelectListItem>();
            SelectedStoreIds = new List<int>();
            AvailableStores = new List<SelectListItem>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryTemplateId { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        public int ParentCategoryId { get; set; }
        public int PictureId { get; set; }
        public int PageSize { get; set; }
        public bool AllowCustomersToSelectPageSize { get; set; }
        public string PageSizeOptions { get; set; }
        public string PriceRanges { get; set; }
        public bool ShowOnHomePage { get; set; }
        public bool IncludeInTopMenu { get; set; }
        public bool SubjectToAcl { get; set; }
        public bool LimitedToStores { get; set; }
        public bool Published { get; set; }
        public bool Deleted { get; set; }
        public int DisplayOrder { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public string Breadcrumb { get; set; }
        public IList<SelectListItem> AvailableCategoryTemplates { get; set; }
        public IList<SelectListItem> AvailableCategories { get; set; }
        public IList<int> SelectedDiscountIds { get; set; }
        public IList<SelectListItem> AvailableDiscounts { get; set; }
        public IList<int> SelectedCustomerRoleIds { get; set; }
        public IList<SelectListItem> AvailableCustomerRoles { get; set; }
        public IList<int> SelectedStoreIds { get; set; }
        public IList<SelectListItem> AvailableStores { get; set; }
    }
}
