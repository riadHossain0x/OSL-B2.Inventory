using OSL_B2.Inventory.Entities.Entities;
using System.Data.Entity;
using OSL_B2.Inventory.Repository.DbContexts.ModelConventions;

namespace OSL_B2.Inventory.Repository.DbContexts
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
            PurchaseModel.Builder(modelBuilder);
            PurchaseDetailModel.Builder(modelBuilder);

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
