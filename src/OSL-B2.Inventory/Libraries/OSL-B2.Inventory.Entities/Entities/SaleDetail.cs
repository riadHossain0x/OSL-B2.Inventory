namespace OSL_B2.Inventory.Entities.Entities
{
    public class SaleDetail
    {
        public long Id { get; set; }
        public Product Product { get; set; }
        public long ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal SalePrice { get; set; }
        public decimal BuyingPrice { get; set; }
        public decimal Total { get; set; }
        public Sale Sale { get; set; }
        public long SaleId { get; set; }
    }
}
