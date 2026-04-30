using DataTransferObject.Model.Statistics;

namespace BusinessLogic.InterfaceBusiness;

public interface IStatisticsLogicLayer
{
    Task<DashBoardStatsDataTransferObject>DashBoardStastisticsAsync();
    
    Task<PeriodStatsDataTransferObject>SimpleperiodStatisticsAsync(DateTime start, DateTime end);

    Task<AllSalesPeriodDataTransferObject> AllSalesStatisticsInPeriodAsync(DateTime start, DateTime end);
}