using System;
using System.Collections.Generic;
using System.Text;

namespace OSL_B2.Inventory.Entities.Entities
{
    public class BaseEntity
    {
        public Status IsActive { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public enum Status
    {
        Active,
        Disactive
    }
}
