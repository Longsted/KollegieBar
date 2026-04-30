namespace DataTransferObject.Model.Statistics;

public class PeriodStatsDataTransferObject
{
    public TopItemDataTransferObject? MostSoldItem { get; set; }
    public int TotalSales { get; set; }
    DateTime StartDate { get; set; }
    DateTime EndDate { get; set; }

}