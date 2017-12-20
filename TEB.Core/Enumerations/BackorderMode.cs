using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEB.Core.Enumerations
{
    public enum BackorderMode
    {
        NoBackorders = 0,
        AllowQtyBelow0 = 1,
        AllowQtyBelow0AndNotifyCustomer = 2,
    }
}
