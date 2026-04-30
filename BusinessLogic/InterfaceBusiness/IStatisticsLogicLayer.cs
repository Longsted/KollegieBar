using DataTransferObject.Model.Statistics;

namespace BusinessLogic.InterfaceBusiness;

public interface IStatisticsLogicLayer
{
    Task<DashBoardStatsDataTransferObject>DashBoardStastisticsAsync();
    
    Task<PeriodStatsDataTransferObject>SimplePeriodStatisticsAsync(DateTime start, DateTime end);

    Task<AllSalesPeriodDataTransferObject> AllSalesStatisticsInPeriodAsync(DateTime start, DateTime end);
}