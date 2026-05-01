namespace Data.Model;

public class Drink
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsAlcoholic { get; set; }
    public List<Liquid> Ingredients { get; set; } = new();
    public string Description { get; set; } = string.Empty;
    public bool IsCustom { get; set; }


    public Drink() { }

    public Drink(string name, bool isAlcoholic, List<Liquid> ingredients, string description)
    {
        Name = name;
        IsAlcoholic = isAlcoholic;
        Ingredients = ingredients;
        Description = description;
    }

    public Drink(string name, bool isAlcoholic, List<Liquid> ingredients, string description, bool isCustom)
       : this(name, isAlcoholic, ingredients, description)
    {
        IsCustom = isCustom;
    }
}
