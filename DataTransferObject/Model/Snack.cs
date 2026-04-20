namespace DataTransferObject.Model;

public class Snack : ProductDto
{
   
    public decimal SalesPrice { get; set; }

    public Snack(string name, decimal costPrice, int stockQuantity, decimal salesPrice) : base(name, costPrice,stockQuantity)
    {
        SalesPrice = salesPrice;
    }

    public Snack()
    {
    }
}