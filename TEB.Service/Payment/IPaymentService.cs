using Braintree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEB.Service
{
    public interface IPaymentService
    {
        string GenerateClientToken(string aCustomerId, string aMerchantAccountid);
        string CreatePurchase(string nonceFromTheClient);
    }
}
