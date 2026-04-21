using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObject.Model
{
    public class SaleStatisticsDataTransferObject
    {
        public int TotalSalesCount { get; set; }
        public decimal TotalRevenue { get; set; }
        public List<ProductSaleDataTransferObject> Products { get; set; }
    }

}
