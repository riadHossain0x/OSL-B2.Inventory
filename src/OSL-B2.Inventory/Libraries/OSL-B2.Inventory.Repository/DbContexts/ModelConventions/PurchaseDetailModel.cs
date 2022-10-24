using OSL_B2.Inventory.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WholeSale.Repository.DbContexts.ModelConventions
{
    internal static class PurchaseDetailModel
    {
        public static void Builder(DbModelBuilder builder)
        {
            builder.Entity<PurchaseDetail>().HasKey(x => x.Id);

            builder.Entity<PurchaseDetail>()
                .HasRequired(x => x.Supplier)
                .WithMany(x => x.PurchaseDetails)
                .HasForeignKey(x => x.SupplierId);

            builder.Entity<PurchaseDetail>()
                .HasRequired(x => x.Purchase)
                .WithMany(x => x.PurchaseDetails)
                .HasForeignKey(x => x.PurchaseId);
        }
    }
}
