using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEB.Core;
using TEB.Core.Common;
using TEB.Core.Domain;
using TEB.Core.ViewModel;
using TEB.Data;

namespace TEB.Service
{
    public interface IShopingCartService
    {
        int InsertBillingAddress(Address model);
        List<CustomerShoppingCartViewModel> GetShoppingCart(int CustomerID, int ShoppingcarttypeID);
        string AddtoCart(int productId, int shoppingCartTypeId, int quantity, int CustomerID);
        List<CustomerShoppingCartViewModel> UpdateShoppingCart(ShoppingCartItemViewModel Model);
    }
}
