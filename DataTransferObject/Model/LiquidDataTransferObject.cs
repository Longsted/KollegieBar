namespace DataTransferObject.Model;

public class LiquidDataTransferObject : ProductDataTransferObject
{
    public int VolumeCl { get; set; }
    public PantDataTransferObject PantDataTransferObject { get; set; }
    public double AlcoholPercentage { get; set; }
    public bool SugarFree { get; set; }

    public bool HasPant => PantDataTransferObject != PantDataTransferObject.None;
    
    public LiquidDataTransferObject()
    {
    }

    public LiquidDataTransferObject(string name, decimal costPrice, int stockQuantity, int volumeCl, PantDataTransferObject pantDataTransferObject, double alcoholPercentage) : base(name, costPrice, stockQuantity)
    {
        VolumeCl = volumeCl;
        PantDataTransferObject = pantDataTransferObject;
        AlcoholPercentage = alcoholPercentage;
        PantDataTransferObject = pantDataTransferObject;
    }

    public LiquidDataTransferObject(string name, decimal costPrice, int stockQuantity, int volumeCl, PantDataTransferObject pantDataTransferObject, bool sugarFree) : base(name, costPrice, stockQuantity)
    {
        VolumeCl = volumeCl;
        PantDataTransferObject = pantDataTransferObject;
        SugarFree = sugarFree;
        PantDataTransferObject = pantDataTransferObject;

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