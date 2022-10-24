using System;

namespace OSL_B2.Inventory.Entities.Entities
{
    public class Purchase
    {
        public long Id { get; set; }
        public string PurchaseNo { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string Details { get; set; }
        public decimal GrandTotal { get; set; }
        public bool IsActive { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
