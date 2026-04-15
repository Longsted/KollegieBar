using DataTransferObject.Model;

namespace Data.Model;


public class LiquidWithAlcohol : LiquidProduct
{
    public double AlcoholPercentage { get; set; }
    
    public LiquidWithAlcohol() : base()
    {
    }

    public LiquidWithAlcohol(string name, decimal costPrice, int stockQuantity, int volumeCl, decimal salesPrice, double alcoholPercentage): 
        base(name, costPrice,stockQuantity, volumeCl, salesPrice)
    {
        AlcoholPercentage = alcoholPercentage;
    }
}