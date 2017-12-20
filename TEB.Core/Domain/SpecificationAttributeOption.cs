using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEB.Core.Domain
{
    public class SpecificationAttributeOption
    {
        public int Id { get; set; }
        public int SpecificationAttributeId { get; set; }
        public string Name { get; set; }
        public string ColorSquaresRgb { get; set; }
        public int DisplayOrder { get; set; }
    }
}
