namespace DataTransferObject.Model;

public class LiquidDataTransferObject : ProductDto
{
    public int VolumeCl { get; set; }
    public double Pant { get; set; }
    public double AlcoholPercentage { get; set; }
    public bool SugarFree { get; set; }

    public LiquidDataTransferObject()
    {
    }

    public LiquidDataTransferObject(string name, decimal costPrice, int stockQuantity, int volumeCl, double pant, double alcoholPercentage) : base(name, costPrice, stockQuantity)
    {
        VolumeCl = volumeCl;
        Pant = pant;
        AlcoholPercentage = alcoholPercentage;
    }

    public LiquidDataTransferObject(string name, decimal costPrice, int stockQuantity, int volumeCl, double pant, bool sugarFree) : base(name, costPrice, stockQuantity)
    {
        VolumeCl = volumeCl;
        Pant = pant;
        SugarFree = sugarFree;
    }

    public LiquidDataTransferObject(string name, decimal costPrice, int stockQuantity, int volumeCl, double alcoholPercentage) : base(name, costPrice, stockQuantity)
    {
        VolumeCl = volumeCl;
        AlcoholPercentage = alcoholPercentage;
    }

    public LiquidDataTransferObject(string name, decimal costPrice, int stockQuantity, int volumeCl, bool sugarFree) : base(name, costPrice, stockQuantity)
    {
        VolumeCl = volumeCl;
        SugarFree = sugarFree;
    }
}