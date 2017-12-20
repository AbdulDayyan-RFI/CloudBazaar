using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEB.Core.Enumerations;

namespace TEB.Core.Common
{
    public class SearchProductModel
    {
        public int pageIndex { get; set; } = 0;
        public int pageSize { get; set; } = int.MaxValue;
        public IList<int> categoryIds { get; set; } = null;
        public int manufacturerId { get; set; } = 0;
        public int storeId { get; set; } = 0;
        public int vendorId { get; set; } = 0;
        public int warehouseId { get; set; } = 0;
        public ProductType? productType { get; set; } = null;
        public bool visibleIndividuallyOnly { get; set; } = false;
        public bool markedAsNewOnly { get; set; } = false;
        public bool? featuredProducts { get; set; } = null;
        public decimal? priceMin { get; set; } = null;
        public decimal? priceMax { get; set; } = null;
        public int productTagId { get; set; } = 0;
        public string keywords { get; set; } = null;
        public bool searchDescriptions { get; set; } = false;
        public bool searchManufacturerPartNumber { get; set; } = true;
        public bool searchSku { get; set; } = true;
        public bool searchProductTags { get; set; } = false;
        public int languageId { get; set; } = 0;
        public IList<int> filteredSpecs { get; set; } = null;
        public ProductSortingEnum orderBy { get; set; } = ProductSortingEnum.Position;
        public bool showHidden { get; set; } = false;
        public bool? overridePublished { get; set; } = null;
    }
}