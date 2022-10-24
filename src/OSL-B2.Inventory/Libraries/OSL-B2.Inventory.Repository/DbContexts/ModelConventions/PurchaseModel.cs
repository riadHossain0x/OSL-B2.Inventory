using OSL_B2.Inventory.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WholeSale.Repository.DbContexts.ModelConventions
{
    internal static class PurchaseModel
    {
        public static void Builder(DbModelBuilder builder)
        {
            builder.Entity<Purchase>().HasKey(x => x.Id);

            builder.Entity<Purchase>().Property(x => x.PurchaseNo)
                .HasColumnType("varchar").HasMaxLength(15).IsRequired();

            builder.Entity<Purchase>().Property(x => x.PurchaseDate).IsRequired();

            builder.Entity<Purchase>().Property(x => x.Details)
                .HasColumnType("varchar").HasMaxLength(256).IsOptional();

            builder.Entity<Purchase>().Property(x => x.GrandTotal)
                .HasColumnType("decimal").HasPrecision(19, 2).IsRequired();

            builder.Entity<Purchase>().Property(x => x.IsActive).IsRequired();

            builder.Entity<Purchase>().Property(x => x.ModifiedBy).IsOptional();

            builder.Entity<Purchase>().Property(x => x.ModifiedDate).IsOptional();

            builder.Entity<Purchase>().Property(x => x.CreatedBy).IsRequired();

            builder.Entity<Purchase>().Property(x => x.CreatedDate).IsRequired();
        }
    }
}
