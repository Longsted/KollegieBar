using DataEntity = Data.Model;
using DataTransfer = DataTransferObject.Model;

namespace BusinessLogic.Mappers;

public class SaleMapper
{

    public static DataTransfer.SaleDataTransferObject Map(DataEntity.Sale sale)
    {
        return new DataTransfer.SaleDataTransferObject
        {
            PriceAtSale = sale.PriceAtSale,
            SaleDate = sale.SaleDate,
            TransactionId = sale.TransactionId,
            ProductId = sale.ProductId,
            DrinkId = sale.DrinkId,

            Product = sale.Product != null ? ProductMapper.Map(sale.Product) : null,

            Drink = sale.Drink != null ? DrinkMapper.Map(sale.Drink) : null

        };
    }
}