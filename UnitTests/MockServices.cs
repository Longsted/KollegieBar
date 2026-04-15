using DataTransferObject.Model;

namespace UnitTests
{
    public class SaleService
    {
        public bool RegisterSale(User user, Product product)
        {
            if (user?.Role != UserRoles.Bartender)
                return false;

            product.StockQuantity -= 1;
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
            if (user?.Role != UserRoles.BoardMember)
                return new List<Product>();

            return _salesData;
        }
    }

    public class IncomingStockService
    {
        public bool RegisterIncomingStock(User user, Product product, int newAmount)
        {
            if (user?.Role != UserRoles.BoardMember)
                return false;

            product.StockQuantity = newAmount;
            return true;
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
            if (user?.Role != UserRoles.BoardMember)
                return new List<Product>();

            return _products
                .Where(p => p.StockQuantity <= _threshold)
                .ToList();
        }
    }

    public class ProductEditingService
    {
        public bool EditProduct(User user, Product product, string newName, int newStock,
            decimal newPrice)
        {
            if (user?.Role != UserRoles.BoardMember)
                return false;

            product.Name = newName;
            product.StockQuantity = newStock;
            product.CostPrice = newPrice;

            return true;
        }
    }

    public class ProductDeletionService
    {
        public bool DeleteProduct(User user, List<Product> products, Product productToDelete)
        {
            if (user?.Role != UserRoles.BoardMember)
                return false;

            return products.Remove(productToDelete);
        }
    }

    public class ProductCategoryService
    {
        public Dictionary<string, List<Product>> GetProductsByCategory(User user, List<Product> products)
        {
            if (user?.Role != UserRoles.BoardMember)
                return new Dictionary<string, List<Product>>();

            return products
                .GroupBy(p => p.GetType().Name) // Her får vi "Snack", "LiquidWithAlcohol" osv.
                .ToDictionary(g => g.Key, g => g.ToList());
        }
    }

    public class DrinkCustomizationService
    {
        public bool CustomizeDrink(User user, Drink drink, double newPrice)
        {
            if (user?.Role != UserRoles.Bartender)
                return false;

            drink.CostPrice = newPrice;

            return true;
        }
    }

    public class WasteRegistrationService
    {
        public bool RegisterWaste(User user, Product product, int amountLost)
        {
            if (user?.Role != UserRoles.Bartender)
                return false;

            if (amountLost <= 0)
                return false;

            product.StockQuantity -= amountLost;

            if (product.StockQuantity < 0)
                product.StockQuantity = 0;

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
            if (user?.Role != UserRoles.BoardMember)
                return false;

            MaxStock = newMaxStock;

            // Adjust product stock if above new max
            if (product.StockQuantity > MaxStock)
                product.StockQuantity = MaxStock;

            return true;
        }
    }

    public class PantService
    {
        public decimal TotalPantIncome { get; private set; }

        public bool RegisterPant(User user, decimal pantAmount)
        {
            if (user?.Role != UserRoles.BoardMember)
                return false;

            if (pantAmount <= 0)
                return false;

            TotalPantIncome += pantAmount;
            return true;
        }

        public bool ResetPant(User user)
        {
            if (user?.Role != UserRoles.BoardMember)
                return false;

            TotalPantIncome = 0;
            return true;
        }
    }

    public class LoginService
    {
        private readonly Dictionary<string, UserRoles> _accounts;

        public LoginService(Dictionary<string, UserRoles> accounts)
        {
            _accounts = accounts;
        }

        public User Login(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return null;

            if (_accounts.TryGetValue(username, out var role))
            {
                return new User { Role = role };
            }

            return null;

        }
    }
}
