using DataEntity = Data.Model;
using DTO = DataTransferObject.Model;

namespace BusinessLogic.Mappers
{
    internal static class ProductMapper
    {
        // Entity -> DataTransferObject
        public static DTO.ProductDto Map(DataEntity.Product product)
        {
            return product switch
            {
                DataEntity.Liquid liquid => LiquidWithAlcoholMapper.Map(liquid),
                DataEntity.Snack snack => SnackMapper.Map(snack),
                DataEntity.Consumables consumables => ConsumablesMapper.Map(consumables),
                _ => throw new NotImplementedException(
                    $"Mapping not implemented for {product.GetType()}"
                )
            };
        }

        // DataTransferObject -> entity
        public static DataEntity.Product Map(DTO.ProductDto productDataTransferObject)
        {
            return productDataTransferObject switch
            {
                DTO.LiquidDataTransferObject liquid => LiquidWithAlcoholMapper.Map(liquid),
                DTO.SnackDataTransferObject snack => SnackMapper.Map(snack),
                DTO.ConsumablesDataTransferObject consumables => ConsumablesMapper.Map(consumables),
                _ => throw new NotImplementedException(
                    $"Mapping not implemented for {productDataTransferObject.GetType()}"
                )
            };
        }
       
        public static void MapToDto(DataEntity.Product entity, DTO.ProductDto dataTransferObject)
        {
            dataTransferObject.Id = entity.Id;
            dataTransferObject.Name = entity.Name;
            dataTransferObject.CostPrice = entity.CostPrice;
            dataTransferObject.StockQuantity = entity.StockQuantity;
        }

        public static void MapToEntity(DTO.ProductDto dataTransferObject, DataEntity.Product entity)
        {
            entity.Id = dataTransferObject.Id;
            entity.Name = dataTransferObject.Name;
            entity.CostPrice = dataTransferObject.CostPrice;
            entity.StockQuantity = dataTransferObject.StockQuantity;
        }
    }
}