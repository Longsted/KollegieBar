namespace Domain.Entities;

public class LiquidWithoutAlcohol : LiquidProduct
{
    public bool SugarFree { get; set; }


    public LiquidWithoutAlcohol(string name, decimal costPrice, int stockQuantity, int volumeCl,decimal salesPrice, bool sugarFree) :
        base(name, costPrice, stockQuantity, volumeCl,salesPrice)
    {
        SugarFree = sugarFree;
    }

    public LiquidWithoutAlcohol()
    {
    }
}