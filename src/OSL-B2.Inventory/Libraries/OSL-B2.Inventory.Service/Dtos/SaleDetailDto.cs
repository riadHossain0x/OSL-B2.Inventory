namespace OSL_B2.Inventory.Service.Dtos
{
    public class SaleDetailDto
    {
        public long Id { get; set; }
        public ProductDto Product { get; set; }
        public long ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal SalePrice { get; set; }
        public decimal BuyingPrice { get; set; }
        public decimal Total { get; set; }
        public SaleDto Sale { get; set; }
        public long SaleId { get; set; }
    }
}
