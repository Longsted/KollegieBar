namespace Data.Model;

public abstract class LiquidProduct : Product
{
    
    public int VolumeCl { get; set; }
    public decimal SalesPrice { get; set; }


    protected LiquidProduct(string name, decimal costPrice, int stockQuantity,int volumeCl, decimal salesPrice) : base(name, costPrice, stockQuantity)
    {
        VolumeCl = volumeCl;
        SalesPrice = salesPrice;
    }

    protected LiquidProduct()
    {
    }
}