using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEB.Data;
using TEB.Core.Domain;
namespace TEB.Service
{
    public class SliderService : ISliderService
    {
        private readonly IGenericRepository<Slider> _sliderRepository;
        public SliderService(IGenericRepository<Slider> sliderRepository)
        {
            _sliderRepository = sliderRepository;
        }

        public object InsertSlider(Slider model)
        {
            var query = @"INSERT [dbo].[Slider] ([Name], [Display_Order], [Is_Active], [CreatedBy], [CreatedDate], 
                        [UpdatedBy], [UpdatedDate]
                        ) 
                        values(@Name, @Display_Order, @Is_Active, @CreatedBy, @CreatedDate, 
                        @UpdatedBy, @UpdatedDate);
                        SELECT SCOPE_IDENTITY()
                        ";
            var param = new
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
            return _sliderRepository.Add(query, param);
        }

        public Slider GetSliderById(int Id)
        {
            string query = "select * from Slider where Id = @Id ";
            var param = new { Id = Id };
            var slider = _sliderRepository.Get(query, param);
            return slider;
        }

        public int UpdateSlider(Slider model)
        {
            var query = @"UPDATE [dbo].[Slider] SET Name=@Name,Display_Order=@Display_Order,
                        Is_Active=@Is_Active,CreatedBy=@CreatedBy,CreatedDate=@CreatedDate,UpdatedBy=@UpdatedBy,UpdatedDate=@UpdatedDate
                        WHERE ID = @ID; SELECT SCOPE_IDENTITY();";
            var param = new
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
            _sliderRepository.Update(query, param);
            return model.ID;
        }

        public int DeleteSlider(int Id)
        {
            var query = @"UPDATE [dbo].[Slider] SET Is_Active=0,UpdatedDate=GETUTCDATE() WHERE ID = @Id";
            var param = new { Id = Id };
            return _sliderRepository.Delete(query, param);
        }

        public IEnumerable<Slider> GetSlidersList()
        {
            string query = "select top(3) * from slider order by ID desc";
            var sliderList = _sliderRepository.GetAll(query, null);
            return sliderList;
        }

    }
}
