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
                CostPrice = entity.CostPrice,
                IsAlcoholic = entity.IsAlcoholic,
                Ingredients = entity.Ingredients
                    .Select(LiquidMapper.Map)
                    .ToList()
            };
        }

        public static DataEntity.Drink Map(DataTransfer.DrinkDataTransferObject dto)
        {
            return new DataEntity.Drink
            {
                Id = dto.Id,
                Name = dto.Name,
                CostPrice = dto.CostPrice,
                IsAlcoholic = dto.IsAlcoholic,
                Ingredients = dto.Ingredients
                    .Select(LiquidMapper.Map)
                    .ToList()
            };
        }

        public static void MapToEntity(DataTransfer.DrinkDataTransferObject dto, DataEntity.Drink entity)
        {
            entity.Name = dto.Name;
            entity.CostPrice = dto.CostPrice;
            entity.IsAlcoholic = dto.IsAlcoholic;
            // Ingredients handled separately
        }
    }
}
