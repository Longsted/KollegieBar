using DataTransferObject.Model;

namespace UnitTests
{
    public class SalesStatisticsService
    {
        private readonly List<ProductDto> _salesData;

        public SalesStatisticsService(List<ProductDto> salesData)
        {
            _salesData = salesData;
        }

        public List<ProductDto> GetStatistics(UserDto userDto)
        {
            if (userDto?.Role != UserRole.BoardMember)
                return new List<ProductDto>();

            return _salesData;
        }
    }

    public class LowInventoryService
    {
        private readonly List<ProductDto> _products;
        private readonly int _threshold;

        public LowInventoryService(List<ProductDto> products, int threshold)
        {
            _products = products;
            _threshold = threshold;
        }

        public List<ProductDto> GetLowInventory(UserDto userDto)
        {
            if (userDto?.Role != UserRole.BoardMember)
                return new List<ProductDto>();

            return _products
                .Where(p => p.StockQuantity <= _threshold)
                .ToList();
        }
    }

    public class ProductEditingService
    {
        public bool EditProduct(UserDto userDto, ProductDto productDto, string newName, int newStock,
            decimal newPrice)
        {
            if (userDto?.Role != UserRole.BoardMember)
                return false;

            productDto.Name = newName;
            productDto.StockQuantity = newStock;
            productDto.CostPrice = newPrice;

            return true;
        }
    }

    public class ProductDeletionService
    {
        public bool DeleteProduct(UserDto userDto, List<ProductDto> products, ProductDto productDtoToDelete)
        {
            if (userDto?.Role != UserRole.BoardMember)
                return false;

            return products.Remove(productDtoToDelete);
        }
    }

    public class ProductCategoryService
    {
        public Dictionary<string, List<ProductDto>> GetProductsByCategory(UserDto userDto, List<ProductDto> products)
        {
            if (userDto?.Role != UserRole.BoardMember)
                return new Dictionary<string, List<ProductDto>>();

            return products
                .GroupBy(p => p.GetType().Name) // Her får vi "Snack", "LiquidWithAlcohol" osv.
                .ToDictionary(g => g.Key, g => g.ToList());
        }
    }

    public class DrinkCustomizationService
    {
        public bool CustomizeDrink(UserDto userDto, DrinkDto drink, double newPrice)
        {
            if (userDto?.Role != UserRole.Bartender)
                return false;

            drink.CostPrice = newPrice;

            return true;
        }
    }

    public class WasteRegistrationService
    {
        public bool RegisterWaste(UserDto userDto, ProductDto productDto, int amountLost)
        {
            if (userDto?.Role != UserRole.Bartender)
                return false;

            if (amountLost <= 0)
                return false;

            productDto.StockQuantity -= amountLost;

            if (productDto.StockQuantity < 0)
                productDto.StockQuantity = 0;

            return true;
        }
    }

    public class StockLimitService
    {
        public int MaxStockQuantity { get; private set; }
        public int MinStockQuantity { get; private set; }

        public StockLimitService(int initialLimit)
        {
            MaxStockQuantity = initialLimit;
            MinStockQuantity = initialLimit;
        }

        public bool UpdateMaxStock(UserDto userDto, ProductDto productDto, int newMaxStock)
        {
            if (userDto?.Role != UserRole.BoardMember)
                return false;

            MaxStockQuantity = newMaxStock;
            
            // Adjust product stock if above new max
            if (productDto.StockQuantity > MaxStockQuantity)
                productDto.StockQuantity = MaxStockQuantity;

            return true;
        }

        public bool UpdateMinStock(UserDto userDto, ProductDto productDto, int newMinStock)
        {
            if (userDto?.Role != UserRole.BoardMember)
                return false;

            MinStockQuantity = newMinStock;

            // Adjust product stock if below new min
            if (productDto.StockQuantity < MinStockQuantity)
                productDto.StockQuantity = MinStockQuantity;

            return true;
        }
    }

    public class PantService
    {
        public decimal TotalPantIncome { get; private set; }

        public bool RegisterPant(UserDto userDto, decimal pantAmount)
        {
            if (userDto?.Role != UserRole.BoardMember)
                return false;

            if (pantAmount <= 0)
                return false;

            TotalPantIncome += pantAmount;
            return true;
        }

        public bool ResetPant(UserDto userDto)
        {
            if (userDto?.Role != UserRole.BoardMember)
                return false;

            TotalPantIncome = 0;
            return true;
        }
    }

    public class LoginService
    {
        private readonly Dictionary<string, UserRole> _accounts;

        public LoginService(Dictionary<string, UserRole> accounts)
        {
            _accounts = accounts;
        }

        public UserDto Login(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return null;

            if (_accounts.TryGetValue(username, out var role))
            {
                return new UserDto { Role = role };
            }

            return null;

        }
    }
}
