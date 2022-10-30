using System;
using System.Collections.Generic;

namespace OSL_B2.Inventory.Service.Dtos
{
    public class SupplierDto : BaseEntityDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string Details { get; set; }
        public List<PurchaseDetailDto> PurchaseDetails { get; set; }
    }
}
