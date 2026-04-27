using DataTransferObject.Model;

namespace UnitTests
{
    public class SalesStatisticsService
    {
        private readonly List<ProductDataTransferObject> _salesData;

        public SalesStatisticsService(List<ProductDataTransferObject> salesData)
        {
            _salesData = salesData;
        }

        public List<ProductDataTransferObject> GetStatistics(UserDataTransferObject userDataTransferObject)
        {
            if (userDataTransferObject?.RoleDataTransferObject != UserRoleDataTransferObject.BoardMember)
                return new List<ProductDataTransferObject>();

            return _salesData;
        }
    }

    public class LowInventoryService
    {
        private readonly List<ProductDataTransferObject> _products;
        private readonly int _threshold;

        public LowInventoryService(List<ProductDataTransferObject> products, int threshold)
        {
            _products = products;
            _threshold = threshold;
        }

        public List<ProductDataTransferObject> GetLowInventory(UserDataTransferObject userDataTransferObject)
        {
            if (userDataTransferObject?.RoleDataTransferObject != UserRoleDataTransferObject.BoardMember)
                return new List<ProductDataTransferObject>();

            return _products
                .Where(p => p.StockQuantity <= _threshold)
                .ToList();
        }
    }

    public class ProductEditingService
    {
        public bool EditProduct(UserDataTransferObject userDataTransferObject, ProductDataTransferObject productDataTransferObject, string newName, int newStock,
            decimal newPrice)
        {
            if (userDataTransferObject?.RoleDataTransferObject != UserRoleDataTransferObject.BoardMember)
                return false;

            productDataTransferObject.Name = newName;
            productDataTransferObject.StockQuantity = newStock;
            productDataTransferObject.CostPrice = newPrice;

            return true;
        }
    }

    public class ProductDeletionService
    {
        public bool DeleteProduct(UserDataTransferObject userDataTransferObject, List<ProductDataTransferObject> products, ProductDataTransferObject productDataTransferObjectToDelete)
        {
            if (userDataTransferObject?.RoleDataTransferObject != UserRoleDataTransferObject.BoardMember)
                return false;

            return products.Remove(productDataTransferObjectToDelete);
        }
    }

    public class ProductCategoryService
    {
        public Dictionary<string, List<ProductDataTransferObject>> GetProductsByCategory(UserDataTransferObject userDataTransferObject, List<ProductDataTransferObject> products)
        {
            if (userDataTransferObject?.RoleDataTransferObject != UserRoleDataTransferObject.BoardMember)
                return new Dictionary<string, List<ProductDataTransferObject>>();

            return products
                .GroupBy(p => p.GetType().Name) // Her får vi "Snack", "LiquidWithAlcohol" osv.
                .ToDictionary(g => g.Key, g => g.ToList());
        }
    }

    public class DrinkCustomizationService
    {
        public bool CustomizeDrink(UserDataTransferObject userDataTransferObject, DrinkDataTransferObject drinkDataTransferObject, double newPrice)
        {
            if (userDataTransferObject?.RoleDataTransferObject != UserRoleDataTransferObject.Bartender)
                return false;

            if (newPrice < 0)
            {
                return false;
            }

            drinkDataTransferObject.CostPrice = newPrice;

            return true;
        }
    }

    public class WasteRegistrationService
    {
        public bool RegisterWaste(UserDataTransferObject userDataTransferObject, ProductDataTransferObject productDataTransferObject, int amountLost)
        {
            if (userDataTransferObject?.RoleDataTransferObject != UserRoleDataTransferObject.Bartender)
                return false;

            if (amountLost <= 0)
                return false;

            productDataTransferObject.StockQuantity -= amountLost;

            if (productDataTransferObject.StockQuantity < 0)
                productDataTransferObject.StockQuantity = 0;

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

        public bool UpdateMaxStock(UserDataTransferObject userDataTransferObject, ProductDataTransferObject productDataTransferObject, int newMaxStock)
        {
            if (userDataTransferObject?.RoleDataTransferObject != UserRoleDataTransferObject.BoardMember)
                return false;

            MaxStockQuantity = newMaxStock;
            
            // Adjust product stock if above new max
            if (productDataTransferObject.StockQuantity > MaxStockQuantity)
                productDataTransferObject.StockQuantity = MaxStockQuantity;

            return true;
        }

        public bool UpdateMinStock(UserDataTransferObject userDataTransferObject, ProductDataTransferObject productDataTransferObject, int newMinStock)
        {
            if (userDataTransferObject?.RoleDataTransferObject != UserRoleDataTransferObject.BoardMember)
                return false;

            MinStockQuantity = newMinStock;

            // Adjust product stock if below new min
            if (productDataTransferObject.StockQuantity < MinStockQuantity)
                productDataTransferObject.StockQuantity = MinStockQuantity;

            return true;
        }
    }

    public class PantService
    {
        public decimal TotalPantIncome { get; private set; }

        public bool RegisterPant(UserDataTransferObject userDataTransferObject, decimal pantAmount)
        {
            if (userDataTransferObject?.RoleDataTransferObject != UserRoleDataTransferObject.BoardMember)
                return false;

            if (pantAmount <= 0)
                return false;

            TotalPantIncome += pantAmount;
            return true;
        }

        public bool ResetPant(UserDataTransferObject userDataTransferObject)
        {
            if (userDataTransferObject?.RoleDataTransferObject != UserRoleDataTransferObject.BoardMember)
                return false;

            TotalPantIncome = 0;
            return true;
        }
    }
}
