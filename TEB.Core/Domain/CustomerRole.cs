using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEB.Core.Domain
{
    public class CustomerRole
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool FreeShipping { get; set; }
        public bool TaxExempt { get; set; }
        public bool Active { get; set; }
        public bool IsSystemRole { get; set; }
        public string SystemName { get; set; }
        public bool EnablePasswordLifetime { get; set; }
        public int PurchasedWithProductId { get; set; }
    }
}
