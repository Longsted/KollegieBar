using Data.Model;

namespace Data.Model;

public class DrinkIngredient
{
    public int Id { get; set; }
    public int DrinkId { get; set; }
    public Drink Drink { get; set; }
    public int LiquidProductId { get; set; }
    public Liquid Liquid { get; set; }
    public int VolumeCl { get; set; }

    public DrinkIngredient() { }

    public DrinkIngredient(int liquidProductId)
    {
        LiquidProductId = liquidProductId;
    }
}
