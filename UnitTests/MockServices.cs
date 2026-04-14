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

    public class ProductEditingService
    {
        public bool EditProduct(User user, Product product, string newName, int newStock, DrinkType newType, decimal newPrice)
        {
            if (user?.Role != UserRole.BoardMember)
                return false;

            product.Name = newName;
            product.Stock = newStock;
            product.Type = newType;
            product.Price = newPrice;

            return true;
        }
    }

    public class ProductDeletionService
    {
        public bool DeleteProduct(User user, List<Product> products, Product productToDelete)
        {
            if (user?.Role != UserRole.BoardMember)
                return false;

            return products.Remove(productToDelete);
        }
    }

    public class ProductCategoryService
    {
        public Dictionary<DrinkType, List<Product>> GetProductsByCategory(User user, List<Product> products)
        {
            if (user?.Role != UserRole.BoardMember)
                return new Dictionary<DrinkType, List<Product>>();

            return products
                .GroupBy(p => p.Type)
                .ToDictionary(g => g.Key, g => g.ToList());
        }
    }

    public class DrinkCustomizationService
    {
        public bool CustomizeDrink(User user, Product product, decimal newPrice, DrinkType newType)
        {
            if (user?.Role != UserRole.Bartender)
                return false;

            product.Price = newPrice;
            product.Type = newType;

            return true;
        }
    }

    public class WasteRegistrationService
    {
        public bool RegisterWaste(User user, Product product, int amountLost)
        {
            if (user?.Role != UserRole.Bartender)
                return false;

            if (amountLost <= 0)
                return false;

            product.Stock -= amountLost;

            if (product.Stock < 0)
                product.Stock = 0;

            return true;
        }
    }

    public class StockLimitService
    {
        public int MaxStock { get; private set; }

        public StockLimitService(int initialMaxStock)
        {
            MaxStock = initialMaxStock;
        }

        public bool UpdateMaxStock(User user, Product product, int newMaxStock)
        {
            if (user?.Role != UserRole.BoardMember)
                return false;

            MaxStock = newMaxStock;

            // Adjust product stock if above new max
            if (product.Stock > MaxStock)
                product.Stock = MaxStock;

            return true;
        }
    }

    public class PantService
    {
        public decimal TotalPantIncome { get; private set; }

        public bool RegisterPant(User user, decimal pantAmount)
        {
            if (user?.Role != UserRole.BoardMember)
                return false;

            if (pantAmount <= 0)
                return false;

            TotalPantIncome += pantAmount;
            return true;
        }

        public bool ResetPant(User user)
        {
            if (user?.Role != UserRole.BoardMember)
                return false;

            TotalPantIncome = 0;
            return true;
        }
    }
}
