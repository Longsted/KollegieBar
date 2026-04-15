namespace Data.Model
{
    public class Sale
    {
        public int SaleId { get; set; } 
        public int ProductId { get; set; }
        public decimal PriceAtSale { get; set; }
        public DateTime Timestamp { get; set; }

        public Sale(int productId, int quantity, decimal PriceAtSale)
        {
            ProductId = productId;
            PriceAtSale = PriceAtSale;
            Timestamp = DateTime.Now;
        }

        public Sale() { }
    }
}
