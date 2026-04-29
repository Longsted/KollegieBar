namespace DataTransferObject.Model;

public abstract class ProductDataTransferObject : ICartItem
{
    public int Id { get; set; }
    public string Name { get; set; } 
    public decimal CostPrice{ get; set; }
    public int StockQuantity { get; set; }

    public int MaxStockQuantity { get; set; }
    public int MinStockQuantity { get; set; }


    protected ProductDataTransferObject(string name, decimal costPrice, int stockQuantity)
    {
        Name = name;
        CostPrice = costPrice;
        StockQuantity = stockQuantity;
    }

    protected ProductDataTransferObject()
    {
    }
}