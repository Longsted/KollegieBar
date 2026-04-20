using DataEntity = Data.Model;
using DTO = DataTransferObject.Model;

namespace BusinessLogic.Mappers
{
    internal static class LiquidMapper
    {
        public static void MapToDto(DataEntity.Liquid entity, DTO.LiquidDataTransferObject dataTransferObject)
        {
            // Base (Product)
            ProductMapper.MapToDto(entity, dataTransferObject);

            // Liquid-specifics
            dataTransferObject.VolumeCl = entity.VolumeCl;
        }

        public static void MapToEntity(DTO.LiquidDataTransferObject dataTransferObject, DataEntity.Liquid entity)
        {
            // Base (Product)
            ProductMapper.MapToEntity(dataTransferObject, entity);

            // Liquid-specifics
            entity.VolumeCl = dataTransferObject.VolumeCl;
        }
    }
}