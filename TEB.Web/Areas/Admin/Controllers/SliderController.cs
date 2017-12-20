using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TEB.Core.Common;
using TEB.Core.Domain;
using TEB.Core.Mapping;
using TEB.Core.ViewModel;
using TEB.Web.Controllers;

namespace TEB.Web.Areas.Admin.Controllers
{
    public class SliderController : BaseController
    {
        // GET: Admin/Slider
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection fc, HttpPostedFileBase file)
        {
            //tbl_details tbl = new tbl_details();
            var allowedExtensions = new[] {
            ".Jpg", ".png", ".jpg", "jpeg"
        };
            //tbl.Id = fc["Id"].ToString();
            //tbl.Image_url = file.ToString(); //getting complete url  
            //tbl.Name = fc["Name"].ToString();
            var fileName = Path.GetFileName(file.FileName); //getting only file name(ex-ganesh.jpg)  
            var ext = Path.GetExtension(file.FileName); //getting the extension(ex-.jpg)  
            if (allowedExtensions.Contains(ext)) //check what type of extension  
            {
                string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  

                var ramdomnumber = new Random().Next(1, 1000);

                string SliderName = "";

                string myfile = ramdomnumber + "_" + SliderName + ext; //appending the name with id  
                                                                       // store the file inside ~/project folder(Img)  
                var path = Path.Combine(Server.MapPath("~/ProductImages"), myfile);
                //tbl.Image_url = path;
                //obj.tbl_details.Add(tbl);
                //obj.SaveChanges();
                file.SaveAs(path);
            }
            else
            {
                ViewBag.message = "Please choose only Image file";
            }
            return View();
        }


        public ActionResult Create()
        {
            var model = new SliderViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(SliderViewModel model, string savecontinue)
        {
            if (ModelState.IsValid)
            {
                bool continueEditing = false;
                if (!String.IsNullOrWhiteSpace(savecontinue))
                    continueEditing = true;

                //Slider
                Slider sliderModel = new Slider();
                sliderModel = SliderMapping.ViewToModel(model);

                TEBApiResponse apiResponse = await Post<Slider>("/Slider/InsertSlider", sliderModel);

                if (apiResponse.IsSuccess)
                {
                    if (continueEditing)
                    {
                        int productid = JsonConvert.DeserializeObject<int>(Convert.ToString(apiResponse.Data));
                        return RedirectToAction("Edit", new { id = productid });
                    }
                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }

        public async Task<ActionResult> Edit(int id)
        {
            SliderViewModel model = new SliderViewModel();
            TEBApiResponse apiResponse = await Get("/Slider/GetSliderById?Id=" + id);
            if (apiResponse.IsSuccess)
            {
                Slider slider = JsonConvert.DeserializeObject<Slider>(Convert.ToString(apiResponse.Data));
                model = SliderMapping.ModelToView(slider);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(SliderViewModel model, string savecontinue)
        {
            if (ModelState.IsValid)
            {
                bool continueEditing = false;
                if (!String.IsNullOrWhiteSpace(savecontinue))
                    continueEditing = true;

                //slider
                Slider slider = new Slider();
                slider = SliderMapping.ViewToModel(model);

                TEBApiResponse apiResponse = await Post<Slider>("/Slider/UpdateSlider", slider);

                if (apiResponse.IsSuccess)
                {
                    if (continueEditing)
                    {
                        //selected tab
                        //SaveSelectedTabName();
                        int sliderid = JsonConvert.DeserializeObject<int>(Convert.ToString(apiResponse.Data));
                        return RedirectToAction("Edit", new { id = sliderid });
                    }
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        public async Task<ActionResult> Delete(int id)
        {
            TEBApiResponse apiResponse = await Delete("/Slider/DeleteSlider?Id=" + id);

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> GetSlidersList()
        {
            TEBApiResponse apiResponse = await Delete("/Slider/GetSlidersList");
            return RedirectToAction("Index");
        }

    }
}