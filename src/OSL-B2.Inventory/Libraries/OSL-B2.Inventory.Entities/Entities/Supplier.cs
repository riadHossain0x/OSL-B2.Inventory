using System;
using System.Collections.Generic;

namespace OSL_B2.Inventory.Entities.Entities
{
    public class Supplier : BaseEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string Details { get; set; }
        public List<PurchaseDetail> PurchaseDetails { get; set; }
    }
}
