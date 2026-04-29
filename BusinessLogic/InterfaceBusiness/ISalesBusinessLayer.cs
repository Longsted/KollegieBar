using Data.Model;
namespace BusinessLogic.InterfaceBusiness;

public interface ISalesBusinessLayer
{
    Task RegisterSaleAsync(List<int> productIds, List<int> drinksId);

    Task AddIngredient(int saleId, int liquidId);

    Task RemoveIngredient(int saleId, int liquidId);
}
