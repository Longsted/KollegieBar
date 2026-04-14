namespace UnitTests
{
    public class SaleService
    {
        public bool RegisterSale(User user, Product product)
        {
            if (user?.Role != UserRole.Bartender)
                return false;

            product.Stock -= 1;
            return true;
        }
    }

    public class SalesStatisticsService
    {
        private readonly List<Product> _salesData;

        public SalesStatisticsService(List<Product> salesData)
        {
            _salesData = salesData;
        }

        public List<Product> GetStatistics(User user)
        {
            if (user?.Role != UserRole.BoardMember)
                return new List<Product>();

            return _salesData;
        }
    }

    public class IncomingStockService
    {
        public bool RegisterIncomingStock(User user, Product product, int newAmount)
        {
            if (user?.Role != UserRole.BoardMember)
                return false;

            product.Stock = newAmount;
            return true;
        }
    }

    public class ProductCreationService
    {
        public Product CreateProduct(User user, string name, int stock, DrinkType type, decimal price)
        {
            if (user?.Role != UserRole.BoardMember)
                return null;

            return new Product
            {
                Name = name,
                Stock = stock,
                Type = type,
                Price = price
            };
        }
    }

    public class LowInventoryService
    {
        private readonly List<Product> _products;
        private readonly int _threshold;

        public LowInventoryService(List<Product> products, int threshold)
        {
            _products = products;
            _threshold = threshold;
        }

        public List<Product> GetLowInventory(User user)
        {
            if (user?.Role != UserRole.BoardMember)
                return new List<Product>();

            return _products
                .Where(p => p.Stock <= _threshold)
                .ToList();
        }
    }
}
