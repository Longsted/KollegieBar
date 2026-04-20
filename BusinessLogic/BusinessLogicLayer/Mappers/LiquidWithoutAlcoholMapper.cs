namespace BusinessLogic.Mappers;

internal static class LiquidWithoutAlcoholMapper
{
    public static DataTransferObject.Model.LiquidWithoutAlcohol Map(Data.Model.LiquidWithoutAlcohol entity)
    {
        return new DataTransferObject.Model.LiquidWithoutAlcohol
        {
            Id = entity.Id,
            Name = entity.Name,
            CostPrice = entity.CostPrice,
            StockQuantity = entity.StockQuantity,
            VolumeCl = entity.VolumeCl,
            SugarFree = entity.SugarFree
        };
    }

    public static Data.Model.LiquidWithoutAlcohol Map(DataTransferObject.Model.LiquidWithoutAlcohol dataTransferObject)
    {
        return new Data.Model.LiquidWithoutAlcohol
        {
            Id = dataTransferObject.Id,
            Name = dataTransferObject.Name,
            CostPrice = dataTransferObject.CostPrice,
            StockQuantity = dataTransferObject.StockQuantity,
            VolumeCl = dataTransferObject.VolumeCl,
            SugarFree = dataTransferObject.SugarFree
        };
    }
}