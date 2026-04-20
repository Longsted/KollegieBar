using Data.Model;
using DataTransferObject.Model;

namespace BusinessLogic.Mappers
{
    internal static class LiquidWithAlcoholMapper
    {
        public static DataTransferObject.Model.LiquidDataTransferObject Map(Data.Model.Liquid entity)
        {
            var dataTransferObject = new DataTransferObject.Model.LiquidDataTransferObject();

            LiquidMapper.MapToDto(entity, dataTransferObject);

            dataTransferObject.AlcoholPercentage = entity.AlcoholPercentage;

            return dataTransferObject;
        }

        public static Data.Model.Liquid Map(DataTransferObject.Model.LiquidDataTransferObject dataTransferObject)
        {
            var entity = new Data.Model.Liquid();

            LiquidMapper.MapToEntity(dataTransferObject, entity);

            entity.AlcoholPercentage = dataTransferObject.AlcoholPercentage;

            return entity;
        }
    }
}