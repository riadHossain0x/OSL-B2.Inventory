using OSL_B2.Inventory.Repository;
using Unity;

namespace OSL_B2.Inventory.Service
{
    public static class ServiceModule
    {
        public static void Register(UnityContainer container)
        {
            container.RegisterType<ICategoryService, CategoryService>();
            container.RegisterType<ICustomerService, CustomerService>();
            container.RegisterType<ISupplierService, SupplierService>();
            container.RegisterType<IProductService, ProductService>();
            container.RegisterType<IPurchaseService, PurchaseService>();
            RepositoryModule.Register(container);
        }
    }
}
