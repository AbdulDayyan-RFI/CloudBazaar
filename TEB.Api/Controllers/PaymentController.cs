using System.Web.Http;
using TEB.Service;

namespace TEB.Api.Controllers
{
    [AllowAnonymous]
    public class PaymentController : BaseApiController
    {
        public readonly IPaymentService _paymentService;
        
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        public IHttpActionResult GenerateClientToken(string aCustomerId, string aMerchantAccountid)
        {
            return RunInSafe(() =>
            {
                var data = _paymentService.GenerateClientToken(aCustomerId, aMerchantAccountid);
                tebResponse.Data = data;
                tebResponse.IsSuccess = true;
                return Ok(tebResponse);
            });
        }

        [HttpPost]
        public IHttpActionResult CreatePurchase(string nonceFromTheClient)
        {
            return RunInSafe(() =>
            {
                var data = _paymentService.CreatePurchase(nonceFromTheClient);
                tebResponse.Data = data;
                tebResponse.IsSuccess = true;
                return Ok(tebResponse);
            });
        }
    }
}
