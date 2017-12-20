using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TEB.Service;

namespace TEB.Api.Controllers
{
    [AllowAnonymous]
    public class SpecificationAttributeController : BaseApiController
    {
        public readonly ISpecificationAttributeService _specificationAttributeService;
        public readonly ISpecificationAttributeOptionService _specificationAttributeOptionService;

        public SpecificationAttributeController(ISpecificationAttributeService specificationAttributeService,ISpecificationAttributeOptionService specificationAttributeOptionService)
        {
            _specificationAttributeService = specificationAttributeService;
            _specificationAttributeOptionService = specificationAttributeOptionService;
        }

        public IHttpActionResult GetSpecificationAttributeOptionsBySpecificationAttribute(int specificationAttributeId)
        {
            return RunInSafe(() =>
            {
                var data = _specificationAttributeOptionService.GetSpecificationAttributeOptionsBySpecificationAttribute(specificationAttributeId);
                tebResponse.Data = data;
                tebResponse.IsSuccess = true;
                return Ok(tebResponse);
            });
        }

        public IHttpActionResult GetSpecificationAttributes()
        {
            return RunInSafe(() =>
            {
                var data = _specificationAttributeService.GetSpecificationAttributes();
                tebResponse.Data = data.Result;
                tebResponse.IsSuccess = true;
                return Ok(tebResponse);
            });
        }
    }
}
