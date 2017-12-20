using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TEB.Core.Domain;
using TEB.Service;

namespace TEB.Api.Controllers
{
    public class SliderController : BaseApiController
    {

        public readonly ISliderService _sliderService;

        public SliderController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }

        [HttpPost]
        public IHttpActionResult InsertSlider(Slider model)
        {
            return RunInSafe(() =>
            {
                var data = _sliderService.InsertSlider(model);
                tebResponse.Data = data;
                tebResponse.IsSuccess = true;
                return Ok(tebResponse);
            });
        }

        public IHttpActionResult GetSliderById(int Id)
        {
            return RunInSafe(() =>
            {
                var data = _sliderService.GetSliderById(Id);
                tebResponse.Data = data;
                tebResponse.IsSuccess = true;
                return Ok(tebResponse);
            });
        }

        [HttpPost]
        public IHttpActionResult UpdateSlider(Slider model)
        {
            return RunInSafe(() =>
            {
                var data = _sliderService.UpdateSlider(model);
                tebResponse.Data = data;
                tebResponse.IsSuccess = true;
                return Ok(tebResponse);
            });
        }

        [HttpDelete]
        public IHttpActionResult DeleteSlider(int Id)
        {
            return RunInSafe(() =>
            {

                var data = _sliderService.DeleteSlider(Id);
                tebResponse.Data = data;
                tebResponse.IsSuccess = true;
                return Ok(tebResponse);
            });
        }


        public IHttpActionResult GetSliderList()
        {
            return RunInSafe(() =>
            {
                var data = _sliderService.GetSlidersList();
                tebResponse.Data = data;
                tebResponse.IsSuccess = true;
                return Ok(tebResponse);
            });
        }

    }
}
