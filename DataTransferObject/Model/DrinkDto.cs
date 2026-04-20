using DataTransferObject.Model;

namespace DataTransferObject.Model;

public class DrinkDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public double CostPrice { get; set; }
    public double SalesPrice { get; set; }

    public bool IsAlcoholic { get; set; }

    public List<DrinkIngredient> Ingredients { get; set; } = new();

    public DrinkDto() { }

    public DrinkDto(string name, double costPrice, double salesPrice, bool isAlcoholic)
    {
        Name = name;
        CostPrice = costPrice;
        SalesPrice = salesPrice;
        IsAlcoholic = isAlcoholic;
    }
}
