using OSL_B2.Inventory.Membership;
using Unity;

namespace OSL_B2.Inventory.Web
{
    public static class Factory<T>
    {
        public static T GetInstance()
        {
            var container = new UnityContainer();
            MembershipModule.Register(container);
            return container.Resolve<T>();
        }
    }
}