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
            SalesPrice = entity.SalesPrice,
            SugarFree = entity.SugarFree
        };
    }

    public static Data.Model.LiquidWithoutAlcohol Map(DataTransferObject.Model.LiquidWithoutAlcohol dto)
    {
        return new Data.Model.LiquidWithoutAlcohol
        {
            Id = dto.Id,
            Name = dto.Name,
            CostPrice = dto.CostPrice,
            StockQuantity = dto.StockQuantity,
            VolumeCl = dto.VolumeCl,
            SalesPrice = dto.SalesPrice,
            SugarFree = dto.SugarFree
        };
    }
}