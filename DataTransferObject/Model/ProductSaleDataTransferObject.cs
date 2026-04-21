using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObject.Model
{
    public class ProductSaleDataTransferObject
    {
        public string ProductName { get; set; }
        public int ProductID { get; set; }
        public int SalesCount { get; set; }
        public decimal TotalRevenue { get; set; }
    }

}
