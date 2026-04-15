namespace DataTransferObject.Model;

public class Drink 
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double CostPrice { get; set; }
    public double SalesPrice { get; set; }
    public List<string> Ingredients { get; set; } // Måske ændre til flere lister til forskellige typer profukter?
    
    public bool IsAlcoholic { get; set; }
}