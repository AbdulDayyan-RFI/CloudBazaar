using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TEB.Core.Common;

namespace TEB.Api.Controllers
{
    public class BaseApiController : ApiController
    {
        public readonly TEBApiResponse tebResponse;
        public BaseApiController()
        {
            tebResponse = new TEBApiResponse();
        }

        [NonAction]
        public IHttpActionResult RunInSafe(Func<IHttpActionResult> fn)
        {
            try
            {
                return fn();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}