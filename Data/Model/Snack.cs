namespace Data.Model;

public class Snack : Product
{
    public Snack(string name, decimal costPrice, int stockQuantity) : base(name, costPrice,stockQuantity)
    {
    }

    public Snack()
    {
    }
}