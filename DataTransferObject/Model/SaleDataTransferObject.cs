namespace DataTransferObject.Model
{
    public class SaleDataTransferObject
    {
        publgit ic int SaleId { get; set; }
        public DateTime SaleDate { get; set; }
        public Guid TransactionId { get; set; }

        public int? ProductId { get; set; }
        public int? DrinkId { get; set; }

        public ProductDataTransferObject? Product { get; set; }
        public DrinkDataTransferObject? Drink { get; set; }

        public SaleDataTransferObject() { }

        public SaleDataTransferObject(DateTime saleDate, Guid transactionId, ProductDataTransferObject product)
        {
            SaleDate = saleDate;
            TransactionId = transactionId;

            Product = product;
            ProductId = product.Id;
        }

        public SaleDataTransferObject(DateTime saleDate, Guid transactionId, DrinkDataTransferObject drink)
        {
            SaleDate = saleDate;
            TransactionId = transactionId;

            Drink = drink;
            DrinkId = drink.Id;
        }
    }
}