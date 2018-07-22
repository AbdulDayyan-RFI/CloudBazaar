using Braintree;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEB.Service
{
    public class PaymentService : IPaymentService
    {
        BraintreeGateway gateway = new BraintreeGateway();

        public PaymentService()
        {
            gateway = new BraintreeGateway
            {
                Environment = ConfigurationManager.AppSettings["Environment"] == "SandBox" ? Braintree.Environment.SANDBOX : Braintree.Environment.PRODUCTION,
                MerchantId = ConfigurationManager.AppSettings["merchant_id"],
                PublicKey = ConfigurationManager.AppSettings["public_key"],
                PrivateKey = ConfigurationManager.AppSettings["private_key"]
            };
        }

        public string GenerateClientToken(string aCustomerId, string aMerchantAccountid)
        {
            var clientToken = gateway.ClientToken.Generate(new ClientTokenRequest { CustomerId = aCustomerId });
            return clientToken;
        }

        public string CreatePurchase(string nonceFromTheClient)
        {
            var request = new TransactionRequest
            {
                Amount = 10.00M,
                PaymentMethodNonce = nonceFromTheClient,
                Options = new TransactionOptionsRequest
                {
                    SubmitForSettlement = true
                }
            };

            Result<Transaction> result = gateway.Transaction.Sale(request);
            //save in db here

            return result.Message;
        }
    }
}
