using System;
using System.Collections.Generic;

namespace OSL_B2.Inventory.Service.Dtos
{
    public class SaleDto : BaseEntityDto
    {
        public long Id { get; set; }
        public CustomerDto Customer { get; set; }
        public long CustomerId { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal GrandTotal { get; set; }
        public List<SaleDetail> SaleDetails { get; set; }
    }
}
