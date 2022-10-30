using System;
using System.Collections.Generic;
using System.Text;

namespace OSL_B2.Inventory.Service.Dtos
{
    public class ProductDto : BaseEntityDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Details { get; set; }
        public int Quantity { get; set; }
        public int Critical_Qty { get; set; }
        public decimal BuyingPrice { get; set; }
        public decimal SalePrice { get; set; }
        public CategoryDto Category { get; set; }
        public long CategoryId { get; set; }
        public List<SaleDetail> SaleDetails { get; set; }
        public List<PurchaseDetailDto> PurchaseDetails { get; set; }
    }
}
