using OSL_B2.Inventory.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSL_B2.Inventory.Repository.DbContexts.ModelConventions
{
    internal static class SaleDetailModel
    {
        public static void Builder(DbModelBuilder builder)
        {
            builder.Entity<SaleDetail>().HasKey(x => x.Id);

            builder.Entity<SaleDetail>()
                .HasRequired(t => t.Product)
                .WithMany(t => t.SaleDetails)
                .HasForeignKey(t => t.ProductId);

            builder.Entity<SaleDetail>().Property(x => x.Quantity).IsRequired();

            builder.Entity<SaleDetail>().Property(x => x.SalePrice)
                .HasColumnType("decimal").HasPrecision(19,2).IsRequired();

            builder.Entity<SaleDetail>().Property(x => x.BuyingPrice)
                .HasColumnType("decimal").HasPrecision(19, 2).IsRequired();

            builder.Entity<SaleDetail>().Property(x => x.Total)
                .HasColumnType("decimal").HasPrecision(19, 2).IsRequired();

            builder.Entity<SaleDetail>()
                .HasRequired(t => t.Sale)
                .WithMany(t => t.SaleDetails)
                .HasForeignKey(t => t.SaleId);
        }
    }
}
