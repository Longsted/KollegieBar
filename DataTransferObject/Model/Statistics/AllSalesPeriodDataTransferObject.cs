namespace DataTransferObject.Model.Statistics;

public class AllSalesPeriodDataTransferObject
{
    
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int SoldCount { get; set; }
    public List<ItemStatsDataTransferObject> Items { get; set; } = new();

}