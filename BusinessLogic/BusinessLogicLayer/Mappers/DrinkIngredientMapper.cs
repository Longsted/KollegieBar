using DataEntity = Data.Model;


namespace BusinessLogic.Mappers
{
    internal static class DrinkIngredientMapper
    {
        public static DataTransferObject.Model.DrinkIngredientDataTransferObject Map(DataEntity.DrinkIngredient entity)
        {
            return new DataTransferObject.Model.DrinkIngredientDataTransferObject
            {
                Id = entity.Id,
                DrinkId = entity.DrinkId,
                LiquidProductId = entity.LiquidProductId,
               

                // only if liquid is loaded
                LiquidDataTransferObject = entity.Liquid != null
                    ? LiquidMapper.Map(entity.Liquid)
                    : null
            };
        }

        public static DataEntity.DrinkIngredient Map(DataTransferObject.Model.DrinkIngredientDataTransferObject dto)
        {
            return new DataEntity.DrinkIngredient
            {
                Id = dto.Id,
                DrinkId = dto.DrinkId,
                LiquidProductId = dto.LiquidProductId,
               
            };
        }
    }
}