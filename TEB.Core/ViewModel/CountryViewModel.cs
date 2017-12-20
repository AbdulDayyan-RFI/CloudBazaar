using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEB.Core.ViewModel
{
    public class CountryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool AllowsBilling { get; set; }
        public bool AllowsShipping { get; set; }
        public string TwoLetterIsoCode { get; set; }
        public string ThreeLetterIsoCode { get; set; }
        public int NumericIsoCode { get; set; }
        public bool SubjectToVat { get; set; }
        public bool Published { get; set; }
        public int DisplayOrder { get; set; }
        public bool LimitedToStores { get; set; }
    }
}
