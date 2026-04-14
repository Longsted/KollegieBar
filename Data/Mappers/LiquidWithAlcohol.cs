using Data.Model;
using DataTransferObject.Model;

namespace Data.Mappers
{
    internal static class LiquidWithAlcoholMapper
    {
        public static DataTransferObject.Model.LiquidWithAlcohol Map(Data.Model.LiquidWithAlcohol entity)
        {
            var dto = new DataTransferObject.Model.LiquidWithAlcohol();

            LiquidProductMapper.MapToDto(entity, dto);

            dto.AlcoholPercentage = entity.AlcoholPercentage;

            return dto;
        }

        public static Data.Model.LiquidWithAlcohol Map(DataTransferObject.Model.LiquidWithAlcohol dto)
        {
            var entity = new Data.Model.LiquidWithAlcohol();

            LiquidProductMapper.MapToEntity(dto, entity);

            entity.AlcoholPercentage = dto.AlcoholPercentage;

            return entity;
        }
    }
}