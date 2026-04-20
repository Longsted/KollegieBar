using DataEntity = Data.Model;
using DTO = DataTransferObject.Model;

namespace BusinessLogic.Mappers
{
    internal static class SnackMapper
    {
        public static DTO.SnackDataTransferObject Map(DataEntity.Snack entity)
        {
            var dataTransferObject = new DTO.SnackDataTransferObject();

            ProductMapper.MapToDto(entity, dataTransferObject);

            return dataTransferObject;
        }

        public static DataEntity.Snack Map(DTO.SnackDataTransferObject dataTransferObject)
        {
            var entity = new DataEntity.Snack();

            ProductMapper.MapToEntity(dataTransferObject, entity);

            return entity;
        }
    }
}