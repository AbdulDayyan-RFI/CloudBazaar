using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEB.Core.Domain
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public int ShoppingCartTypeId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public string AttributesXml { get; set; }
        public decimal CustomerEnteredPrice { get; set; }
        public int Quantity { get; set; }
        public DateTime RentalStartDateUtc { get; set; }
        public DateTime RentalEndDateUtc { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
    }
}
