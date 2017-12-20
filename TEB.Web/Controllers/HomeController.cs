using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using TEB.Core.Common;
using TEB.Core.Domain;
using System.Linq;

namespace TEB.Web.Controllers
{
    public class HomeController : BaseController
    {
        public async Task<ActionResult> Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        
    }
}