﻿using System;

namespace OSL_B2.Inventory.Entities.Entities
{
    public class Category
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
