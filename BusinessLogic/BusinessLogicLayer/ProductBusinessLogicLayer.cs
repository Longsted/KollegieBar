using Data.Repositories;
using DataTransferObject.Model;

namespace BusinessLogic.BusinessLogicLayer;

public class ProductBusinessLogicLayer
{
    private readonly ProductRepository _repository;

    public ProductBusinessLogicLayer(ProductRepository repository)
    {
        _repository = repository;
    }

    public void CreateSnack(Snack snack)
    {
        _repository.CreateSnack(snack);
    }

    public void CreateLiquidWithAlcohol(LiquidWithAlcohol liquidWithAlcohol)
    {
        _repository.CreateLiquidWithAlcohol(liquidWithAlcohol);
    }

    public void CreateLiquidWithoutAlcohol(LiquidWithoutAlcohol liquidWithoutAlcohol)
    {
        _repository.CreateLiquidWithoutAlcohol(liquidWithoutAlcohol);
    }

    public void CreateConsumables(Consumables consumables)
    {
        _repository.CreateConsumables(consumables);
    }
}