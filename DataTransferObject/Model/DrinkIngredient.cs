using DataTransferObject.Model;

namespace DataTransferObject.Model;

public class DrinkIngredient
{
    public int Id { get; set; }

    public int DrinkId { get; set; }
    public DrinkDto Drink { get; set; }

    public int LiquidProductId { get; set; }
    public LiquidProductDto LiquidProductDto { get; set; }

    public int VolumeCl { get; set; }

    public DrinkIngredient() { }

    public DrinkIngredient(int liquidProductId, int volumeCl)
    {
        LiquidProductId = liquidProductId;
        VolumeCl = volumeCl;
    }
}
