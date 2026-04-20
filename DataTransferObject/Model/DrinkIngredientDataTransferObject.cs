using DataTransferObject.Model;

namespace DataTransferObject.Model;

public class DrinkIngredientDataTransferObject
{
    public int Id { get; set; }

    public int DrinkId { get; set; }
    public DrinkDataTransferObject DrinkDataTransferObject { get; set; }

    public int LiquidProductId { get; set; }
    public LiquidDataTransferObject LiquidDataTransferObject { get; set; }

    public int VolumeCl { get; set; }

    public DrinkIngredientDataTransferObject() { }

    public DrinkIngredientDataTransferObject(int liquidProductId, int volumeCl)
    {
        LiquidProductId = liquidProductId;
        VolumeCl = volumeCl;
    }
}
