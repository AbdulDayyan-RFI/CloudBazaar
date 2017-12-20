using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEB.Core.ViewModel
{
    public class StateProvinceViewModel
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public int Published { get; set; }
        public int DisplayOrder { get; set; }
    }
}
