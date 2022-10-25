﻿using Microsoft.AspNet.Identity.EntityFramework;

namespace OSL_B2.Inventory.Membership.DbContexts
{
    public class Role : IdentityRole<long, UserRole>
    {
        public Role() { }
        public Role(string name) { Name = name; }
    }
}