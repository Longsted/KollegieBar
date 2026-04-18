namespace DataTransferObject.Model;

public class LiquidWithoutAlcohol : LiquidProductDto
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