using DataEntity = Data.Model;
using DTO = DataTransferObject.Model;

namespace BusinessLogic.Mappers
{
    internal static class ConsumablesMapper
    {
        public static DTO.ConsumablesDataTransferObject Map(DataEntity.Consumables entity)
        {
            var dataTransferObject = new DTO.ConsumablesDataTransferObject();

            ProductMapper.MapToDto(entity, dataTransferObject);

            dataTransferObject.Description = entity.Description;

            return dataTransferObject;
        }

        public static DataEntity.Consumables Map(DTO.ConsumablesDataTransferObject dto)
        {
            var entity = new DataEntity.Consumables();

            ProductMapper.MapToEntity(dto, entity);

            entity.Description = dto.Description;

            return entity;
        }
    }
}