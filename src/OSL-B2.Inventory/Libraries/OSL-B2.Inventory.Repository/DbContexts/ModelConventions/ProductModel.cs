using OSL_B2.Inventory.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSL_B2.Inventory.Repository.DbContexts.ModelConventions
{
    internal static class ProductModel
    {
        public static void Builder(DbModelBuilder builder)
        {
            builder.Entity<Product>().HasKey(x => x.Id);

            builder.Entity<Product>().Property(x => x.Name)
                .HasColumnType("varchar").HasMaxLength(50).IsRequired();

            builder.Entity<Product>().Property(x => x.Details)
                .HasColumnType("varchar").HasMaxLength(256).IsOptional();

            builder.Entity<Product>().Property(x => x.Image)
                .HasColumnType("varchar").HasMaxLength(256).IsOptional();

            builder.Entity<Product>().Property(x => x.Quantity).IsOptional();

            builder.Entity<Product>().Property(x => x.Critical_Qty).IsOptional();

            builder.Entity<Product>().Property(x => x.BuyingPrice)
                .HasColumnType("decimal").HasPrecision(19, 2).IsOptional();

            builder.Entity<Product>().Property(x => x.SalePrice)
                .HasColumnType("decimal").HasPrecision(19, 2).IsOptional();

            builder.Entity<Product>()
                .HasRequired(t => t.Category)
                .WithMany(t => t.Products)
                .HasForeignKey(t => t.CategoryId);

            builder.Entity<Product>().Property(x => x.IsActive).IsRequired();

            builder.Entity<Product>().Property(x => x.ModifiedBy).IsOptional();

            builder.Entity<Product>().Property(x => x.ModifiedDate).IsOptional();

            builder.Entity<Product>().Property(x => x.CreatedBy).IsRequired();

            builder.Entity<Product>().Property(x => x.CreatedDate).IsRequired();
        }
    }
}
