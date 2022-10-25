using OSL_B2.Inventory.Entities.Entities;
using System.Data.Entity;
using System.Threading.Tasks;

namespace OSL_B2.Inventory.Repository.DbContexts
{
    public interface IIMSDbContext
    {
        DbSet<Category> Categories { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<PurchaseDetail> PurchaseDetails { get; set; }
        DbSet<Purchase> Purchases { get; set; }
        DbSet<SaleDetail> SaleDetails { get; set; }
        DbSet<Sale> Sales { get; set; }
        DbSet<Supplier> Suppliers { get; set; }
        void SaveChanges();
        Task SaveChangesAsync();
    }
}