using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEB.Core.Domain;
using TEB.Core.ViewModel;

namespace TEB.Core.Mapping
{
    public class SliderMapping
    {
        public static Slider ViewToModel(SliderViewModel model)
        {
            Slider slider = new Slider
            {
                ID = model.ID,
                Name = model.Name,
                Display_Order = model.Display_Order,
                Is_Active = model.Is_Active,
                CreatedBy = model.CreatedBy,
                CreatedDate = model.CreatedDate,
                UpdatedBy = model.UpdatedBy,
                UpdatedDate = model.UpdatedDate,

            };
            return slider;
        }
        public static SliderViewModel ModelToView(Slider model)
        {
            SliderViewModel SliderViewModel = new SliderViewModel
            {
                ID = model.ID,
                Name = model.Name,
                Display_Order = model.Display_Order,
                Is_Active = model.Is_Active,
                CreatedBy = model.CreatedBy,
                CreatedDate = model.CreatedDate,
                UpdatedBy = model.UpdatedBy,
                UpdatedDate = model.UpdatedDate,

            };
            return SliderViewModel;
        }

    }
}
