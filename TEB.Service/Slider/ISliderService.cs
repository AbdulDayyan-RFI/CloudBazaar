using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEB.Core.Domain;

namespace TEB.Service
{
    public interface ISliderService
    {
        object InsertSlider(TEB.Core.Domain.Slider model);
        Slider GetSliderById(int Id);
        int UpdateSlider(TEB.Core.Domain.Slider model);
        int DeleteSlider(int Id);
        IEnumerable<Slider> GetSlidersList();
        //Task<IEnumerable<Product>> SearchProducts(SearchProductModel model);
    }
}
