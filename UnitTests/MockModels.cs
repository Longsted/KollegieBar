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

    public class InventoryService
    {
        public void RegisterSale(Product product)
        {
            product.Stock -= 1;
        }
    }

    public class StatisticsService
    {
        private readonly List<Product> _salesData;

        public StatisticsService(List<Product> salesData)
        {
            _salesData = salesData;
        }

        public List<Product> GetSalesStatistics()
        {
            return _salesData;
        }
    }
}
