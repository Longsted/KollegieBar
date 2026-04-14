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
}
