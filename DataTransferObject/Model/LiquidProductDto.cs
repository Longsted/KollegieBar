namespace DataTransferObject.Model;

public abstract class LiquidProductDto : ProductDto
{
    
    public int VolumeCl { get; set; }
    public decimal SalesPrice { get; set; }

    public decimal Pant { get; set; }


    protected LiquidProductDto(string name, decimal costPrice, int stockQuantity,int volumeCl, decimal salesPrice) : base(name, costPrice, stockQuantity)
    {
        VolumeCl = volumeCl;
        SalesPrice = salesPrice;
    }

    protected LiquidProductDto(string name, decimal Pant, decimal costPrice, int stockQuantity, int volumeCl, decimal salesPrice) : base(name, costPrice, stockQuantity)
    {
        VolumeCl = volumeCl;
        SalesPrice = salesPrice;
        this.Pant = Pant;
    }

    protected LiquidProductDto()
    {
    }
}