﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEB.Core.ViewModel
{
    public class ProductWarehouseInventoryViewModel
    {
        public int EmailAccountId { get; set; }
        public int ProductId { get; set; }
        public int WarehouseId { get; set; }
        public int StockQuantity { get; set; }
        public int ReservedQuantity { get; set; }

    }
}