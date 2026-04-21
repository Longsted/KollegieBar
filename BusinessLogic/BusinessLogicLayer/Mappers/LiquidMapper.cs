using DataEntity = Data.Model;
using DTO = DataTransferObject.Model;

namespace BusinessLogic.Mappers
{
    internal static class LiquidMapper
    {
        public static DTO.LiquidDataTransferObject Map(DataEntity.Liquid entity)
        {
            var dto = new DTO.LiquidDataTransferObject();
            MapToDto(entity, dto);
            return dto;
        }

        private static void MapToDto(DataEntity.Liquid entity, DTO.LiquidDataTransferObject dto)
        {
            ProductMapper.MapToDto(entity, dto);

            dto.VolumeCl = entity.VolumeCl;

            dto.AlcoholPercentage = entity.AlcoholPercentage;
            dto.SugarFree = entity.SugarFree;
        }

        public static DataEntity.Liquid Map(DTO.LiquidDataTransferObject dto)
        {
            var entity = new DataEntity.Liquid();
            MapToEntity(dto, entity);
            return entity;
        }
        
        private static void MapToEntity(DTO.LiquidDataTransferObject dto, DataEntity.Liquid entity)
        {
            ProductMapper.MapToEntity(dto, entity);

            entity.VolumeCl = dto.VolumeCl;
            
            entity.AlcoholPercentage = dto.AlcoholPercentage; 
            entity.SugarFree = dto.SugarFree;
        }
    }
}