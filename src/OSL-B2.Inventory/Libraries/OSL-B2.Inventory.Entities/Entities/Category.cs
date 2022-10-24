using System;
using System.Collections.Generic;

namespace OSL_B2.Inventory.Entities.Entities
{
    public class Category : BaseEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; }
    }
}
