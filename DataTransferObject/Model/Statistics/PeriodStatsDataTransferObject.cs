namespace DataTransferObject.Model.Statistics;

public class PeriodStatsDataTransferObject
{
    public ItemStatsDataTransferObject? MostSoldItem { get; set; }
    public int TotalSales { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

}