namespace DataTransferObject.Model.Statistics;

public class DashBoardStatsDataTransferObject
{
  
    
    public int TotalSalesThisFriday { get; set; }
    
    public ItemStatsDataTransferObject? MostSoldItem { get; set; }
    
    public int LowStockCount { get; set; }
   
 
}