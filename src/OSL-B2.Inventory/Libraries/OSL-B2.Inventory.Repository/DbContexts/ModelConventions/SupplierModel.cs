using OSL_B2.Inventory.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OSL_B2.Inventory.Repository.DbContexts.ModelConventions
{
    internal static class SupplierModel
    {
        public static void Builder(DbModelBuilder builder)
        {
            builder.Entity<Supplier>().HasKey(x => x.Id);

            builder.Entity<Supplier>().Property(x => x.Name)
            .HasColumnType("varchar").HasMaxLength(50).IsRequired();

            builder.Entity<Supplier>().Property(x => x.Mobile)
            .HasColumnType("varchar").HasMaxLength(15).IsRequired();

            builder.Entity<Supplier>().Property(x => x.Address)
            .HasColumnType("varchar").HasMaxLength(256).IsOptional();

            builder.Entity<Supplier>().Property(x => x.Details)
            .HasColumnType("varchar").HasMaxLength(256).IsOptional();

            builder.Entity<Supplier>().Property(x => x.IsActive).IsRequired();

            builder.Entity<Supplier>().Property(x => x.ModifiedBy).IsOptional();

            builder.Entity<Supplier>().Property(x => x.ModifiedDate).IsOptional();

            builder.Entity<Supplier>().Property(x => x.CreatedBy).IsRequired();

            builder.Entity<Supplier>().Property(x => x.CreatedDate).IsRequired();
        }
    }
}
