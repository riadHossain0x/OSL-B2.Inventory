using OSL_B2.Inventory.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSL_B2.Inventory.Repository.DbContexts.ModelConventions
{
    internal static class SaleModel
    {
        public static void Builder(DbModelBuilder builder)
        {
            builder.Entity<Sale>().HasKey(x => x.Id);

            builder.Entity<Sale>()
                .HasRequired(t => t.Customer)
                .WithMany(t => t.Sales)
                .HasForeignKey(t => t.CustomerId);

            builder.Entity<Sale>().Property(x => x.GrandTotal)
                .HasColumnType("decimal").HasPrecision(19, 2).IsRequired();

            builder.Entity<Sale>().Property(x => x.SaleDate).IsRequired();

            builder.Entity<Sale>().Property(x => x.IsActive).IsRequired();

            builder.Entity<Sale>().Property(x => x.ModifiedBy).IsOptional();

            builder.Entity<Sale>().Property(x => x.ModifiedDate).IsOptional();

            builder.Entity<Sale>().Property(x => x.CreatedBy).IsRequired();

            builder.Entity<Sale>().Property(x => x.CreatedDate).IsRequired();
        }
    }
}
