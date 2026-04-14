namespace DataTransferObject.Model;


public class LiquidWithAlcohol : LiquidProduct
{
    public decimal AlcoholPercentage { get; set; }
    
    public LiquidWithAlcohol() : base()
    {
    }

    public LiquidWithAlcohol(string name, decimal costPrice, int stockQuantity, int volumeCl, decimal salesPrice, decimal alcoholPercentage): 
        base(name, costPrice,stockQuantity, volumeCl, salesPrice)
    {
        AlcoholPercentage = alcoholPercentage;
    }
}