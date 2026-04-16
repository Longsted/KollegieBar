using DataEntity = Data.Model;
using DTO = DataTransferObject.Model;

namespace Data.Mappers
{
    internal static class ProductMapper
    {
        // entity -> dto
        public static DTO.Product Map(DataEntity.Product product)
        {
            return product switch
            {
                DataEntity.LiquidWithAlcohol lwa => LiquidWithAlcoholMapper.Map(lwa),
                DataEntity.LiquidWithoutAlcohol lwo => LiquidWithoutAlcoholMapper.Map(lwo),
                DataEntity.Snack s => SnackMapper.Map(s),
                DataEntity.Consumables c => ConsumablesMapper.Map(c),
                _ => throw new NotImplementedException(
                    $"Mapping not implemented for {product.GetType()}"
                )
            };
        }

        //dto -> entity
        public static DataEntity.Product Map(DTO.Product product)
        {
            return product switch
            {
                DTO.LiquidWithAlcohol lwa => LiquidWithAlcoholMapper.Map(lwa),
                DTO.LiquidWithoutAlcohol lwo => LiquidWithoutAlcoholMapper.Map(lwo),
                DTO.Snack s => SnackMapper.Map(s),
                DTO.Consumables c => ConsumablesMapper.Map(c),
                _ => throw new NotImplementedException(
                    $"Mapping not implemented for {product.GetType()}"
                )
            };
        }

       
        public static void MapToDto(DataEntity.Product entity, DTO.Product dto)
        {
            dto.Id = entity.Id;
            dto.Name = entity.Name;
            dto.CostPrice = entity.CostPrice;
            dto.StockQuantity = entity.StockQuantity;
        }

        public static void MapToEntity(DTO.Product dto, DataEntity.Product entity)
        {
            entity.Id = dto.Id;
            entity.Name = dto.Name;
            entity.CostPrice = dto.CostPrice;
            entity.StockQuantity = dto.StockQuantity;
        }
    }
}