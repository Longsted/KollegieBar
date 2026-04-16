using DataEntity = Data.Model;
using DTO = DataTransferObject.Model;

namespace Data.Mappers
{
    internal static class LiquidProductMapper
    {
        public static void MapToDto(DataEntity.LiquidProduct entity, DTO.LiquidProduct dto)
        {
            // base (Product)
            ProductMapper.MapToDto(entity, dto);

            // liquid-specifikke felter
            dto.VolumeCl = entity.VolumeCl;
            dto.SalesPrice = entity.SalesPrice;
            dto.Pant = entity.Pant;
        }

        public static void MapToEntity(DTO.LiquidProduct dto, DataEntity.LiquidProduct entity)
        {
            // base (Product)
            ProductMapper.MapToEntity(dto, entity);

            // liquid-specifikke felter
            entity.VolumeCl = dto.VolumeCl;
            entity.SalesPrice = dto.SalesPrice;
            entity.Pant = dto.Pant;
        }
    }
}