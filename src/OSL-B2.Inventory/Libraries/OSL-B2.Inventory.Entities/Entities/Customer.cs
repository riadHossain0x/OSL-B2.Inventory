using System;
using System.Collections.Generic;

namespace OSL_B2.Inventory.Entities.Entities
{
    public class Customer : BaseEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public List<Sale> Sales { get; set; }
    }
}
