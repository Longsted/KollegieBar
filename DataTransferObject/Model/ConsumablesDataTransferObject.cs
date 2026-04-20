namespace DataTransferObject.Model;

public class ConsumablesDataTransferObject : ProductDto
{
    public string? Description { get; set; }

    public ConsumablesDataTransferObject(string name, decimal costPrice, int stockQuantity, string? description) 
        : base(name, costPrice, stockQuantity)
    {
        Description = description;
    }

    public ConsumablesDataTransferObject()
    {
    }
}