using System;

namespace OSL_B2.Inventory.Entities.Entities
{
    public class Sale
    {
        public long Id { get; set; }
        public Customer Customer { get; set; }
        public long CustomerId { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal GrandTotal { get; set; }
        public bool IsActive { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
