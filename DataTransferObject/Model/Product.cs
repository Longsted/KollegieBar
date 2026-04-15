namespace DataTransferObject.Model;

public abstract class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal CostPrice{ get; set; }
    public int StockQuantity{ get; set => StockQuantity += value; }


    protected Product(string name, decimal costPrice, int stockQuantity)
    {
        Name = name;
        CostPrice = costPrice;
        StockQuantity = stockQuantity;
    }

    protected Product()
    {
    }
}