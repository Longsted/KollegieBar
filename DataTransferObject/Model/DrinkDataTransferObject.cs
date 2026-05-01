namespace DataTransferObject.Model;

public class DrinkDataTransferObject : ICartItem
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsAlcoholic { get; set; }
    public List<LiquidDataTransferObject> Ingredients { get; set; } = new();
    public string Description { get; set; } = string.Empty;
    public bool IsCustom { get; set; }

    public DrinkDataTransferObject() { }

    public DrinkDataTransferObject(string name, bool isAlcoholic, List<LiquidDataTransferObject> ingredients, string description)
    {
        Name = name;
        IsAlcoholic = isAlcoholic;
        Ingredients = ingredients;
        Description = description;
    }

    public DrinkDataTransferObject(string name, bool isAlcoholic, List<LiquidDataTransferObject> ingredients, string description, bool isCustom)
        : this(name, isAlcoholic, ingredients, description)
    {
        IsCustom = isCustom;
    }
}