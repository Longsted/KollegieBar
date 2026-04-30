namespace Data.Model
{
    public class Sale
    {
        public int SaleId { get; private set; }
        public DateTime SaleDate { get; set; }
        public Guid TransactionId { get; set; }

        // Foreign key to Product
        public int? ProductId { get; set; }
        public Product? Product { get; set; }

        // Foreign key to Drink
        public int? DrinkId { get; set; }
        public Drink? Drink { get; set; }

        // Constructor for Product sale
        public Sale(DateTime saleDate, Guid transactionId, Product product)
        {
            SaleDate = saleDate;
            TransactionId = transactionId;

            Product = product;
            ProductId = product.Id;
        }

        // Constructor for Drink sale
        public Sale(DateTime saleDate, Guid transactionId, Drink drink)
        {
            SaleDate = saleDate;
            TransactionId = transactionId;

            Drink = drink;
            DrinkId = drink.Id;
        }

        public Sale() { }
    }
}