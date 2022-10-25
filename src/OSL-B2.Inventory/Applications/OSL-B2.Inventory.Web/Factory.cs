using OSL_B2.Inventory.Membership;
using OSL_B2.Inventory.Repository;
using System.ComponentModel;
using Unity;

namespace OSL_B2.Inventory.Web
{
    public static class Factory<T>
    {
        public static T GetInstance()
        {
            var container = new UnityContainer();
            MembershipModule.Register(container);
            RepositoryModule.Register(container);
            return container.Resolve<T>();
        }
    }
}