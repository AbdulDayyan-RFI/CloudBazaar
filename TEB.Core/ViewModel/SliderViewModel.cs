using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEB.Core.ViewModel
{
    public class SliderViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Display_Order { get; set; }
        public int Is_Active { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public Nullable<DateTime> UpdatedDate { get; set; }
    }
}
