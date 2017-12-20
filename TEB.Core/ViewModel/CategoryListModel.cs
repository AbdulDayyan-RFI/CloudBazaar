using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TEB.Core.Common;
using TEB.Core.Domain;

namespace TEB.Core.ViewModel
{
    public class CategoryListModel
    {
        public CategoryListModel()
        {
            AvailableStores = new List<SelectListItem>();
        }
        public string SearchCategoryName { get; set; }
        public int SearchStoreId { get; set; }
        public IList<SelectListItem> AvailableStores { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public bool showHidden { get; set; }
        public DataSourceResult GridData { get; set; }
    }
}
