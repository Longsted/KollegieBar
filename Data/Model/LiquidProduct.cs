namespace Data.Model;

public abstract class LiquidProduct : Product
{
    
    public int VolumeCl { get; set; }
    public decimal SalesPrice { get; set; }

    public Pant Pant { get; set; }

    protected LiquidProduct(string name, decimal costPrice, int stockQuantity,int volumeCl, decimal salesPrice) : base(name, costPrice, stockQuantity)
    {
        VolumeCl = volumeCl;
        SalesPrice = salesPrice;
    }

    protected LiquidProduct(string name, Pant Pant, decimal costPrice, int stockQuantity, int volumeCl, decimal salesPrice) : base(name, costPrice, stockQuantity)
    {
        VolumeCl = volumeCl;
        SalesPrice = salesPrice;
        this.Pant = Pant;
    }

    protected LiquidProduct()
    {
    }
}