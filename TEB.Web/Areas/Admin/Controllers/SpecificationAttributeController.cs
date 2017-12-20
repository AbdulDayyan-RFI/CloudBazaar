using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TEB.Core.Common;
using TEB.Core.Domain;
using TEB.Web.Controllers;

namespace TEB.Web.Areas.Admin.Controllers
{
    public class SpecificationAttributeController : BaseController
    {
        // GET: Admin/SpecificationAttribute
        public async Task<ActionResult> GetOptionsByAttributeId(string attributeId)
        {
            if (String.IsNullOrEmpty(attributeId))
                throw new ArgumentNullException("attributeId");

            TEBApiResponse apiResponse = await Get("/SpecificationAttribute/GetSpecificationAttributeOptionsBySpecificationAttribute?specificationAttributeId=" + Convert.ToInt32(attributeId));
            if (apiResponse.IsSuccess)
            {
                List<SpecificationAttributeOption> list = JsonConvert.DeserializeObject<List<SpecificationAttributeOption>>(Convert.ToString(apiResponse.Data));
                var result = (from o in list
                              select new { id = o.Id, name = o.Name }).ToList();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return View();
        }

       

    }
}