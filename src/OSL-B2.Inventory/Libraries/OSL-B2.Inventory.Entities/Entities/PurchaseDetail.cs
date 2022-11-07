using System;

namespace OSL_B2.Inventory.Entities.Entities
{
    public class PurchaseDetail
    {
        public long Id { get; set; }
        public Supplier Supplier { get; set; }
        public long SupplierId { get; set; }
        public Product Product { get; set; }
        public long ProductId { get; set; }
        public Purchase Purchase { get; set; }
        public long PurchaseId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
    }
}
