namespace DataTransferObject.Model
{
    public class Sale
    {
        public int SaleId { get; set; }
        public decimal PriceAtSale { get; set; }
        public DateTime SaleDate { get; set; }
        public Guid TransactionId { get; set; }

        public int? ProductId { get; set; }
        public int? DrinkId { get; set; }

        public ProductDto? Product { get; set; }
        public DrinkDataTransferObject? Drink { get; set; }

        public Sale() { }

        public Sale(decimal priceAtSale, DateTime saleDate, Guid transactionId, int productId)
        {
            PriceAtSale = priceAtSale;
            SaleDate = saleDate;
            TransactionId = transactionId;
            ProductId = productId;
        }

        public Sale(decimal priceAtSale, DateTime saleDate, Guid transactionId, int drinkId, bool isDrink)
        {
            PriceAtSale = priceAtSale;
            SaleDate = saleDate;
            TransactionId = transactionId;
            DrinkId = drinkId;
        }
    }
}