using BusinessLogic.InterfaceBusiness;
using Data.Model;
using Data.UnitOfWork;
using DataTransferObject.Model;
using DataTransferObject.Model.Statistics;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BusinessLogic.BusinessLogicLayer;

public class StatisticsLogicLayer : IStatisticsLogicLayer
{
    private readonly IUnitOfWork _unitOfWork;

    public StatisticsLogicLayer(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<DashBoardStatsDataTransferObject> DashBoardStastisticsAsync()
    {
        var today = DateTime.UtcNow.Date;

        int difference = (7 + (today.DayOfWeek - DayOfWeek.Friday)) % 7;
        var targetFriday = today.AddDays(-difference);
        var totalSalesThisFriday = await _unitOfWork.Sales.GetSalesByDateRange(targetFriday, targetFriday.AddDays(1));


        var mostSoldProduct = GetMostSoldGroup(totalSalesThisFriday, s => s.ProductId);

        var mostSoldDrink = GetMostSoldGroup(totalSalesThisFriday, s => s.DrinkId);

        var winner = GetWinner(mostSoldProduct, mostSoldDrink);

        var mostSoldDataTransferObject = MapToTopItemDataTransferObject(winner);

        var totalSalesCount = totalSalesThisFriday.Count;

        var products = await _unitOfWork.Products.GetAllAsync();
        var lowStockCount = products.Count(p => p.StockQuantity <= p.MinStockQuantity);

        return new DashBoardStatsDataTransferObject
        {
            TotalSalesThisFriday = totalSalesCount,
            LowStockCount = lowStockCount,
            MostSoldItem = mostSoldDataTransferObject
        };
    }

    public async Task<PeriodStatsDataTransferObject> SimplePeriodStatisticsAsync(DateTime start, DateTime end)
    {
        var salesPeriod = await _unitOfWork.Sales.GetSalesByDateRange(start, end);

        var soldCount = salesPeriod.Count;

        var mostSoldProduct = GetMostSoldGroup(salesPeriod, s => s.ProductId);

        var mostSoldDrink = GetMostSoldGroup(salesPeriod, s => s.DrinkId);

        var winner = GetWinner(mostSoldProduct, mostSoldDrink);

        var mostSoldDataTransferObject = MapToTopItemDataTransferObject(winner);

        return new PeriodStatsDataTransferObject
        {
            MostSoldItem = mostSoldDataTransferObject,
            TotalSales = soldCount,
            StartDate = start,
            EndDate = end,
        };
    }

    public async Task<AllSalesPeriodDataTransferObject> AllSalesStatisticsInPeriodAsync(DateTime start, DateTime end)
    {
        var salesPeriod = await _unitOfWork.Sales.GetSalesByDateRange(start, end);
        var soldCount = salesPeriod.Count;

        var items = salesPeriod.Select(s => new
            {
                Name = s.Product?.Name ?? s.Drink?.Name ?? "Unknown"
            }).GroupBy(x => x.Name).Select(g => new ItemStatsDataTransferObject()
            {
                Name = g.Key,
                Count = g.Count()
            })
            .OrderByDescending(x => x.Count).ToList();

        return new AllSalesPeriodDataTransferObject
        {
            StartDate = start,
            EndDate = end,
            SoldCount = soldCount,
            Items = items
        };
    }


    //------------------- HELPER METHODS------------------------------
    private IGrouping<T, Sale>? GetMostSoldGroup<T>(
        IEnumerable<Sale> sales, Func<Sale, T?> keySelector)
    {
        return sales
            .Where(s => keySelector(s) != null)
            .GroupBy(s => keySelector(s)!) //exclamaition mark to say I know this isn't null         
            .OrderByDescending(g => g.Count())
            .FirstOrDefault();
    }

    private IGrouping<T, Sale>? GetWinner<T>(
        IGrouping<T, Sale>? group1,
        IGrouping<T, Sale>? group2)
    {
        if (group1 != null && group2 != null)
        {
            return group1.Count() >= group2.Count()
                ? group1
                : group2;
        }

        return group1 ?? group2;
    }

    private ItemStatsDataTransferObject? MapToTopItemDataTransferObject(
        IGrouping<int?, Sale>? winner)
    {
        if (winner == null)
            return null;

        var firstSale = winner.First();

        var name = firstSale.Product?.Name
                   ?? firstSale.Drink?.Name
                   ?? "Unknown";

        return new ItemStatsDataTransferObject
        {
            Name = name,
            Count = winner.Count()
        };
    }
}