using Data.Model;
using DataTransferObject.Model;

namespace BusinessLogic.Mappers
{
    internal static class LiquidWithAlcoholMapper
    {
        public static DataTransferObject.Model.LiquidWithAlcohol Map(Data.Model.LiquidWithAlcohol entity)
        {
            var dataTransferObject = new DataTransferObject.Model.LiquidWithAlcohol();

            LiquidMapper.MapToDto(entity, dataTransferObject);

            dataTransferObject.AlcoholPercentage = entity.AlcoholPercentage;

            return dataTransferObject;
        }

        public static Data.Model.LiquidWithAlcohol Map(DataTransferObject.Model.LiquidWithAlcohol dataTransferObject)
        {
            var entity = new Data.Model.LiquidWithAlcohol();

            LiquidMapper.MapToEntity(dataTransferObject, entity);

            entity.AlcoholPercentage = dataTransferObject.AlcoholPercentage;

            return entity;
        }
    }
}