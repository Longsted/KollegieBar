using DataEntity = Data.Model;
using DataTransfer = DataTransferObject.Model;

namespace BusinessLogic.Mappers
{
    internal static class DrinkMapper
    {
        public static DataTransfer.DrinkDataTransferObject Map(DataEntity.Drink entity)
        {
            return new DataTransfer.DrinkDataTransferObject
            {
                Id = entity.Id,
                Name = entity.Name,
                IsAlcoholic = entity.IsAlcoholic,
                Ingredients = entity.Ingredients
                    .Select(LiquidMapper.Map)
                    .ToList(),
                Description = entity.Description,
                IsCustom = entity.IsCustom
            };
        }

        public static DataEntity.Drink Map(DataTransfer.DrinkDataTransferObject dto)
        {
            return new DataEntity.Drink
            {
                Id = dto.Id,
                Name = dto.Name,
                IsAlcoholic = dto.IsAlcoholic,
                Ingredients = dto.Ingredients
                    .Select(LiquidMapper.Map)
                    .ToList(),
                Description = dto.Description,
                IsCustom = dto.IsCustom
            };
        }

        public static void MapToEntity(DataTransfer.DrinkDataTransferObject dto, DataEntity.Drink entity)
        {
            entity.Name = dto.Name;
            entity.IsAlcoholic = dto.IsAlcoholic;
            // Ingredients handled separately
            entity.Description = dto.Description;
        }
    }
}
