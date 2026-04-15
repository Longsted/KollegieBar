using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObject.Model
{
    public class SaleStatistics
    {
        public int TotalSalesCount { get; set; }
        public decimal TotalRevenue { get; set; }
        public List<ProductSale> Products { get; set; }
    }

}
