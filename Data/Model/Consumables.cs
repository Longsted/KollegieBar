namespace Data.Model;

public class Consumables : Product
{
    public string? Description { get; set; }

    public Consumables(string name, decimal costPrice, int stockQuantity, string? description)
        : base(name, costPrice, stockQuantity)
    {
        Description = description;
    }

    public Consumables()
    {
    }
}