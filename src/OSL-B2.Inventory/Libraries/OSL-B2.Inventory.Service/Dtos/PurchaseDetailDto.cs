using System;

namespace OSL_B2.Inventory.Service.Dtos
{
    public class PurchaseDetailDto
    {
        public long Id { get; set; }
        public SupplierDto Supplier { get; set; }
        public long SupplierId { get; set; }
        public ProductDto Product { get; set; }
        public long ProductId { get; set; }
        public PurchaseDto Purchase { get; set; }
        public long PurchaseId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
    }
}
