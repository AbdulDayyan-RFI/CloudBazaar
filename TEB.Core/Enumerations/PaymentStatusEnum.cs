using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEB.Core.Enumerations
{
    public enum PaymentStatusEnum
    {
        Pending = 10,
        Authorized = 20,
        Paid = 30,
        PartiallyRefunded = 35,
        Refunded = 40,
        Voided = 50,
    }
}
