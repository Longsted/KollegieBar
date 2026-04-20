namespace Data.Model;

public class Liquid : Product
{
    public int VolumeCl { get; set; }
    public double Pant { get; set; }
    public double AlcoholPercentage { get; set; }
    public bool SugarFree { get; set; }

    public Liquid()
    {
    }

    public Liquid(string name, decimal costPrice, int stockQuantity, int volumeCl, double pant, double alcoholPercentage) : base(name, costPrice, stockQuantity)
    {
        VolumeCl = volumeCl;
        Pant = pant;
        AlcoholPercentage = alcoholPercentage;
    }

    public Liquid(string name, decimal costPrice, int stockQuantity, int volumeCl, double pant, bool sugarFree) : base(name, costPrice, stockQuantity)
    {
        VolumeCl = volumeCl;
        Pant = pant;
        SugarFree = sugarFree;
    }

    public Liquid(string name, decimal costPrice, int stockQuantity, int volumeCl, double alcoholPercentage) : base(name, costPrice, stockQuantity)
    {
        VolumeCl = volumeCl;
        AlcoholPercentage = alcoholPercentage;
    }

    public Liquid(string name, decimal costPrice, int stockQuantity, int volumeCl, bool sugarFree) : base(name, costPrice, stockQuantity)
    {
        VolumeCl = volumeCl;
        SugarFree = sugarFree;
    }
}