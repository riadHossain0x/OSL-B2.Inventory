using OSL_B2.Inventory.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WholeSale.Repository.DbContexts.ModelConventions;

namespace WholeSale.Repository.DbContexts
{
    public class IMSDbContext : DbContext
    {
        public IMSDbContext() 
            : base("DefaultConnection")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            ProductModel.Builder(modelBuilder);
            CategoryModel.Builder(modelBuilder);
            SupplierModel.Builder(modelBuilder);
            CustomerModel.Builder(modelBuilder);
            SaleModel.Builder(modelBuilder);
            SaleDetailModel.Builder(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleDetail> SaleDetails { get; set; }
    }
}
