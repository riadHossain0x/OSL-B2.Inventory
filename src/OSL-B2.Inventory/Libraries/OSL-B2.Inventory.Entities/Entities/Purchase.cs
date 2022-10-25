using System;
using System.Collections.Generic;

namespace OSL_B2.Inventory.Entities.Entities
{
    public class Purchase : BaseEntity
    {
        public long Id { get; set; }
        public string PurchaseNo { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string Details { get; set; }
        public decimal GrandTotal { get; set; }
        public List<PurchaseDetail> PurchaseDetails { get; set; }
    }
}
