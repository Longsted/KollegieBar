namespace DataTransferObject.Model
{
    public class SaleDataTransferObject
    {
        public int SaleId { get; private set; }
        public decimal PriceAtSale { get; set; }
        public DateTime SaleDate { get; set; }
        public Guid TransactionId { get; set; }

        public int? ProductId { get; set; }
        public int? DrinkId { get; set; }

        public ProductDataTransferObject? Product { get; set; }
        public DrinkDataTransferObject? Drink { get; set; }

        public SaleDataTransferObject() { }

        public SaleDataTransferObject(decimal priceAtSale, DateTime saleDate, Guid transactionId, ProductDataTransferObject product)
        {
            PriceAtSale = priceAtSale;
            SaleDate = saleDate;
            TransactionId = transactionId;

            Product = product;
            ProductId = product.Id;
        }

        public SaleDataTransferObject(decimal priceAtSale, DateTime saleDate, Guid transactionId, DrinkDataTransferObject drink)
        {
            PriceAtSale = priceAtSale;
            SaleDate = saleDate;
            TransactionId = transactionId;

            Drink = drink;
            DrinkId = drink.Id;
        }
    }
}