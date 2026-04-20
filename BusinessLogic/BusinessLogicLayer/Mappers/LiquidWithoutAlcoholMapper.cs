namespace BusinessLogic.Mappers;

internal static class LiquidWithoutAlcoholMapper
{
    public static DataTransferObject.Model.LiquidDataTransferObject Map(Data.Model.Liquid entity)
    {
        return new DataTransferObject.Model.LiquidDataTransferObject
        {
            Id = entity.Id,
            Name = entity.Name,
            CostPrice = entity.CostPrice,
            StockQuantity = entity.StockQuantity,
            VolumeCl = entity.VolumeCl,
            SugarFree = entity.SugarFree
        };
    }

    public static Data.Model.Liquid Map(DataTransferObject.Model.LiquidDataTransferObject dataTransferObject)
    {
        return new Data.Model.Liquid
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