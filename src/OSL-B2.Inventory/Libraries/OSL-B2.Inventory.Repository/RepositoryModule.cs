using OSL_B2.Inventory.Repository.DbContexts;
using Unity;

namespace OSL_B2.Inventory.Repository
{
    public static class RepositoryModule
    {
        public static void Register(UnityContainer container)
        {
            container.RegisterType<IIMSDbContext, IMSDbContext>(TypeLifetime.Scoped);
            container.RegisterType<ICategoryRepository, CategoryRepository>();
            container.RegisterType<ICustomerRepository, CustomerRepository>();
        }
    }
}
