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
        public int ProductId { get; set; }
        public ProductDto ProductDto { get; set; }
        public Sale(int saleId, decimal priceAtSale, DateTime saleDate, Guid transactionId, int productId)
        {
            SaleId = saleId;
            PriceAtSale = priceAtSale;
            SaleDate = saleDate;
            TransactionId = transactionId;
            ProductId = productId;

        }


        public Sale() { }
    }
}

