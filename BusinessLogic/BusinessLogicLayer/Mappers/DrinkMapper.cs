using DataEntity = Data.Model;
using DataTransfer = DataTransferObject.Model;

namespace BusinessLogic.Mappers
{
    internal static class DrinkMapper
    {
        public static DataTransfer.DrinkDataTransferObject Map(DataEntity.Drink entity)
        {
            var dto = new DataTransfer.DrinkDataTransferObject
            {
                Id = entity.Id,
                Name = entity.Name,
                CostPrice = entity.CostPrice,
                IsAlcoholic = entity.IsAlcoholic,

                Ingredients = entity.Ingredients
                    .Select(DrinkIngredientMapper.Map)
                    .ToList()
            };

            return dto;
        }

        public static DataEntity.Drink Map(DataTransfer.DrinkDataTransferObject dto)
        {
            var entity = new DataEntity.Drink
            {
                Id = dto.Id,
                Name = dto.Name,
                CostPrice = dto.CostPrice,
                IsAlcoholic = dto.IsAlcoholic,

                Ingredients = dto.Ingredients
                    .Select(DrinkIngredientMapper.Map)
                    .ToList()
            };

            return entity;
        }
        public static void MapToEntity(
            DataTransfer.DrinkDataTransferObject dto,
            DataEntity.Drink entity)
        {
            entity.Name = dto.Name;
            entity.CostPrice = dto.CostPrice;
            entity.IsAlcoholic = dto.IsAlcoholic;

            // ingredients handled in businesslayer
        }
        public static void MapToDataTransferObject(
            DataEntity.Drink entity,
            DataTransfer.DrinkDataTransferObject dto)
        {
            dto.Id = entity.Id;
            dto.Name = entity.Name;
            dto.CostPrice = entity.CostPrice;
            dto.IsAlcoholic = entity.IsAlcoholic;

            dto.Ingredients = entity.Ingredients
                .Select(DrinkIngredientMapper.Map)
                .ToList();
        }
    }
    
}