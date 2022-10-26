using System;
using System.Collections.Generic;

namespace OSL_B2.Inventory.Service.Dtos
{
    public class PurchaseDto : BaseEntityDto
    {
        public long Id { get; set; }
        public string PurchaseNo { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string Details { get; set; }
        public decimal GrandTotal { get; set; }
        public List<PurchaseDetailDto> PurchaseDetails { get; set; }
    }
}
