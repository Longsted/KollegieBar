using Data.Model;

namespace Data.Model;

public class Drink
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public double CostPrice { get; set; }
    public double SalesPrice { get; set; }

    public bool IsAlcoholic { get; set; }

    public List<DrinkIngredient> Ingredients { get; set; } = new();

    public Drink() { }

    public Drink(string name, double costPrice, double salesPrice, bool isAlcoholic)
    {
        Name = name;
        CostPrice = costPrice;
        SalesPrice = salesPrice;
        IsAlcoholic = isAlcoholic;
    }
}
