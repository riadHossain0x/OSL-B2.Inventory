using System;
using System.Collections.Generic;

namespace OSL_B2.Inventory.Entities.Entities
{
    public class Sale : BaseEntity
    {
        public long Id { get; set; }
        public Customer Customer { get; set; }
        public long CustomerId { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal DiscountTotal { get; set; }
        public decimal GrandTotal { get; set; }
        public List<SaleDetail> SaleDetails { get; set; }
    }
}
