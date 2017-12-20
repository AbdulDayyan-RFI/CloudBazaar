using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEB.Core.Domain
{
    public class StoreMapping
    {
        public int Id { get; set; }
        public int EntityId { get; set; }
        public string EntityName { get; set; }
        public int StoreId { get; set; }
    }
}
