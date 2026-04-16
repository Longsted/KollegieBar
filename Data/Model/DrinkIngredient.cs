using Data.Model;


namespace Data.Model;

public class DrinkIngredient
{
    public int DrinkId { get; set; }
    public Drink Drink { get; set; }

    public int LiquidProductId { get; set; }
    public LiquidProduct LiquidProduct { get; set; }

    public int VolumeCl { get; set; }

    public DrinkIngredient() { }

    public DrinkIngredient(int liquidProductId, int volumeCl)
    {
        LiquidProductId = liquidProductId;
        VolumeCl = volumeCl;
    }
}