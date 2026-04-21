namespace DataTransferObject.Model;

public class SnackDataTransferObject : ProductDataTransferObject
{
    public SnackDataTransferObject(string name, decimal costPrice, int stockQuantity) : base(name, costPrice,stockQuantity)
    {
    }

    public SnackDataTransferObject()
    {
    }
}