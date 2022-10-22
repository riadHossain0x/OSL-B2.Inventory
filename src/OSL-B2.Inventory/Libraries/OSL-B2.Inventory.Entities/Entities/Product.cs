using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OSL_B2.Inventory.Entities.Entities
{
    public class Product
    {
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        [MaxLength(256)]
        public string Image { get; set; }

        [MaxLength(256)]
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
