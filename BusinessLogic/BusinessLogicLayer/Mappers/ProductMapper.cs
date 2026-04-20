using DataEntity = Data.Model;
using DTO = DataTransferObject.Model;

namespace BusinessLogic.Mappers
{
    internal static class ProductMapper
    {
        // entity -> dto
        public static DTO.ProductDto Map(DataEntity.Product product)
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
        public static DataEntity.Product Map(DTO.ProductDto productDto)
        {
            return productDto switch
            {
                DTO.LiquidWithAlcohol lwa => LiquidWithAlcoholMapper.Map(lwa),
                DTO.LiquidWithoutAlcohol lwo => LiquidWithoutAlcoholMapper.Map(lwo),
                DTO.Snack s => SnackMapper.Map(s),
                DTO.Consumables c => ConsumablesMapper.Map(c),
                _ => throw new NotImplementedException(
                    $"Mapping not implemented for {productDto.GetType()}"
                )
            };
        }

       
        public static void MapToDto(DataEntity.Product entity, DTO.ProductDto dto)
        {
            dto.Id = entity.Id;
            dto.Name = entity.Name;
            dto.CostPrice = entity.CostPrice;
            dto.StockQuantity = entity.StockQuantity;
        }

        public static void MapToEntity(DTO.ProductDto dto, DataEntity.Product entity)
        {
            entity.Id = dto.Id;
            entity.Name = dto.Name;
            entity.CostPrice = dto.CostPrice;
            entity.StockQuantity = dto.StockQuantity;
        }
    }
}