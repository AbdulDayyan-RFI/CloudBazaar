using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEB.Core.Enumerations
{
    public enum ShipmentStatusEnum
    {
        ShippingNotRequired = 10,
        NotYetShipped = 20,
        PartiallyShipped = 25,
        Shipped = 30,
        Delivered = 40,
    }
}
