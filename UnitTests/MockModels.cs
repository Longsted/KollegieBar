namespace UnitTests
{
    public enum UserRole
    {
        Bartender,
        BoardMember
    }

    public class User
    {
        public UserRole? Role { get; set; }
    }

    public enum DrinkType
    {
        Beer,
        Cider,
        Soda,
        Spirit
    }

    public class Product
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public DrinkType Type { get; set; }
        public decimal Price { get; set; }
    }
}
