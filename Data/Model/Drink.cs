using Data.Model;

namespace Data.Model;

public class Drink
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public decimal CostPrice { get; set; }
    public decimal SalesPrice { get; set; }

    public bool IsAlcoholic { get; set; }

    public List<DrinkIngredient> Ingredients { get; set; } = new();

    public Drink() { }

    public Drink(string name, decimal costPrice, decimal salesPrice, bool isAlcoholic)
    {
        Name = name;
        CostPrice = costPrice;
        SalesPrice = salesPrice;
        IsAlcoholic = isAlcoholic;
    }
}
