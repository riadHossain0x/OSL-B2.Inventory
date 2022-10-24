using System;
using System.Collections.Generic;
using System.Text;

namespace OSL_B2.Inventory.Entities.Entities
{
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Details { get; set; }
        public int Quantity { get; set; }
        public int Critical_Qty { get; set; }
        public decimal BuyingPrice { get; set; }
        public decimal SalePrice { get; set; }
        public bool IsActive { get; set; }
        public Category Category { get; set; }
        public long CategoryId { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
