namespace DataTransferObject.Model
{
    public class Sale
    {
        public int SaleId { get; set; }
        public decimal PriceAtSale { get; set; }
        public DateTime SaleDate { get; set; }

        //Because we want to be able to track the individual transactions, we need to have a unique identifier
        //for each transaction. This will allow us to link the sale to the specific products that were sold.
        public Guid TransactionId { get; set; }

        //Foreign key to Product
        public int? ProductId { get; set; }
        public ProductDto? Product { get; set; }

        //Foreign key to Drink
        public int? DrinkId { get; set; }
        public DrinkDataTransferObject? Drink { get; set; }

        //Constructor for Sale
        public Sale(int saleId, decimal priceAtSale, DateTime saleDate, Guid transactionId, ProductDto product)
        {
            SaleId = saleId;
            PriceAtSale = priceAtSale;
            SaleDate = saleDate;
            TransactionId = transactionId;
            Product = product;
        }

        //Overloaded constructor for when we want to create a sale for a drink
        public Sale(int saleId, decimal priceAtSale, DateTime saleDate, Guid transactionId, DrinkDataTransferObject drink)
        {
            SaleId = saleId;
            PriceAtSale = priceAtSale;
            SaleDate = saleDate;
            TransactionId = transactionId;
            Drink = drink;
        }

        public Sale() { }
    }
}

