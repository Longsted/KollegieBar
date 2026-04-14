using DataTransferObject.Model;

namespace BusinessLogic.BusinessLogicLayer;

public class ProductBusinessLogicLayer
{
    public void CreateSnack(Snack snack)
    {
        Data.Repositories.ProductRepository.CreateSnack(snack);
    }
    
    public void CreateLiquidWithAlcohol(LiquidWithAlcohol liquidWithAlcohol)
    {
        Data.Repositories.ProductRepository.CreateSnack(liquidWithAlcohol);
    }
    
    public void CreateLiquidWithoutAlcohol(LiquidWithoutAlcohol liquidWithoutAlcohol)
    {
        Data.Repositories.ProductRepository.CreateLiquidWithoutAlcohol(liquidWithoutAlcohol);
    }
    
    public void CreateConsumables(Consumables consumables)
    {
        Data.Repositories.ProductRepository.CreateConsumables(consumables);
    }
    
}