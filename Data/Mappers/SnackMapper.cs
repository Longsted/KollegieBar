using DataEntity = Data.Model;
using DTO = DataTransferObject.Model;

namespace Data.Mappers
{
    internal static class SnackMapper
    {
        public static DTO.Snack Map(DataEntity.Snack entity)
        {
            var dto = new DTO.Snack();

            ProductMapper.MapToDto(entity, dto);

            dto.SalesPrice = entity.SalesPrice;

            return dto;
        }

        public static DataEntity.Snack Map(DTO.Snack dto)
        {
            var entity = new DataEntity.Snack();

            ProductMapper.MapToEntity(dto, entity);

            entity.SalesPrice = dto.SalesPrice;

            return entity;
        }
    }
}