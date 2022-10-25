using OSL_B2.Inventory.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSL_B2.Inventory.Repository.DbContexts.ModelConventions
{
    internal static class CustomerModel
    {
        public static void Builder(DbModelBuilder builder)
        {
            builder.Entity<Customer>().HasKey(x => x.Id);

            builder.Entity<Customer>().Property(x => x.Name)
                .HasColumnType("varchar").HasMaxLength(50).IsRequired();

            builder.Entity<Customer>().Property(x => x.Email).IsOptional();

            builder.Entity<Customer>().Property(x => x.Mobile)
                .HasColumnType("varchar").HasMaxLength(15).IsRequired();

            builder.Entity<Customer>().Property(x => x.IsActive).IsRequired();

            builder.Entity<Customer>().Property(x => x.ModifiedBy).IsOptional();

            builder.Entity<Customer>().Property(x => x.ModifiedDate).IsOptional();

            builder.Entity<Customer>().Property(x => x.CreatedBy).IsRequired();

            builder.Entity<Customer>().Property(x => x.CreatedDate).IsRequired();
        }
    }
}
