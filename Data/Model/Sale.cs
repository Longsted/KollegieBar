namespace Data.Model
{
    public class Sale
    {
        public int SaleId { get; private set; }
        public decimal PriceAtSale { get; set; }
        public DateTime SaleDate { get; set; }
        public Guid TransactionId { get; set; }

        // Foreign key to Product
        public int? ProductId { get; set; }
        public Product? Product { get; set; }

        // Foreign key to Drink
        public int? DrinkId { get; set; }
        public Drink? Drink { get; set; }

        // Constructor for Product sale
        public Sale(decimal priceAtSale, DateTime saleDate, Guid transactionId, Product product)
        {
            PriceAtSale = priceAtSale;
            SaleDate = saleDate;
            TransactionId = transactionId;

            Product = product;
            ProductId = product.Id;
        }

        // Constructor for Drink sale
        public Sale(decimal priceAtSale, DateTime saleDate, Guid transactionId, Drink drink)
        {
            PriceAtSale = priceAtSale;
            SaleDate = saleDate;
            TransactionId = transactionId;

            Drink = drink;
            DrinkId = drink.Id;
        }

        private Sale() { }
    }
}