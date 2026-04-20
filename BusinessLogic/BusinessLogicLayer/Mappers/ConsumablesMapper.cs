using DataEntity = Data.Model;
using DTO = DataTransferObject.Model;

namespace BusinessLogic.Mappers
{
    internal static class ConsumablesMapper
    {
        public static DTO.Consumables Map(DataEntity.Consumables entity)
        {
            var dto = new DTO.Consumables();

            ProductMapper.MapToDto(entity, dto);

            dto.Description = entity.Description;

            return dto;
        }

        public static DataEntity.Consumables Map(DTO.Consumables dto)
        {
            var entity = new DataEntity.Consumables();

            ProductMapper.MapToEntity(dto, entity);

            entity.Description = dto.Description;

            return entity;
        }
    }
}