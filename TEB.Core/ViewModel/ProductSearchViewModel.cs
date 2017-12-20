using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TEB.Core.ViewModel
{
    public class ProductSearchViewModel
    {
        public ProductSearchViewModel()
        {
            AvailableCategories = new List<SelectListItem>();
            AvailableManufacturers = new List<SelectListItem>();
            AvailableStores = new List<SelectListItem>();
            AvailableWarehouses = new List<SelectListItem>();
            AvailableVendors = new List<SelectListItem>();
            AvailableProductTypes = new List<SelectListItem>();
            AvailablePublishedOptions = new List<SelectListItem>();
        }

        [AllowHtml]
        public string SearchProductName { get; set; }
        [AllowHtml]
        public string GoDirectlyToSku { get; set; }

        public int SearchCategoryId { get; set; }
        public bool SearchIncludeSubCategories { get; set; }
        public int SearchManufacturerId { get; set; }
        public int SearchStoreId { get; set; }
        public int SearchVendorId { get; set; }
        public int SearchWarehouseId { get; set; }
        public int SearchProductTypeId { get; set; }
        public int SearchPublishedId { get; set; }
        public bool IsLoggedInAsVendor { get; set; }
        public bool AllowVendorsToImportProducts { get; set; }

        public IList<SelectListItem> AvailableCategories { get; set; }
        public IList<SelectListItem> AvailableManufacturers { get; set; }
        public IList<SelectListItem> AvailableStores { get; set; }
        public IList<SelectListItem> AvailableWarehouses { get; set; }
        public IList<SelectListItem> AvailableVendors { get; set; }
        public IList<SelectListItem> AvailableProductTypes { get; set; }
        public IList<SelectListItem> AvailablePublishedOptions { get; set; }
    }
}
