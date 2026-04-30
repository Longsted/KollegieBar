using Data.Model;
using Data.UnitOfWork;
using DataTransferObject.Model;
using DataTransferObject.Model.Statistics;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BusinessLogic.BusinessLogicLayer;

public class StatisticsLogicLayer
{
    private readonly IUnitOfWork _unitOfWork;

    public StatisticsLogicLayer(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<DashBoardStatsDataTransferObject> DashBoardStastisticsAsync()
    {
        var allSales = await _unitOfWork.Sales.GetAllWithRelationsAsync();

        var today = DateTime.UtcNow.Date;


        int difference = (7 + (today.DayOfWeek - DayOfWeek.Friday)) % 7;
        var targetFriday = today.AddDays(-difference);


        //filter sales that happened last friday 
        var totalSalesThisFriday = allSales.Where(s =>
            s.SaleDate >= targetFriday &&
            s.SaleDate < targetFriday.AddDays(1)).ToList();


        var mostSoldProduct = GetMostSoldGroup(totalSalesThisFriday, s => s.ProductId);

        var mostSoldDrink = GetMostSoldGroup(totalSalesThisFriday, s => s.DrinkId);


        IGrouping<int?, Sale>? winner = null;

        if (mostSoldProduct != null && mostSoldDrink != null)
        {
            winner = mostSoldProduct.Count() >= mostSoldDrink.Count()
                ? mostSoldProduct
                : mostSoldDrink;
        }
        else
        {
            winner = mostSoldProduct ?? mostSoldDrink;
        }

        TopItemDataTransferObject? mostSoldDataTransferObject = null;

        if (winner != null)
        {
            var firstSale = winner.First();

            var name = firstSale.Product?.Name ?? firstSale.Drink?.Name ?? "Unknown";

            mostSoldDataTransferObject = new TopItemDataTransferObject
            {
                Name = name,
                Count = winner.Count()
            };
        }


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

    public async Task PeriodStatistics(DateTime start, DateTime end)
    {
        
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
}