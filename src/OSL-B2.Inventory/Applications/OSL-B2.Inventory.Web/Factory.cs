﻿using OSL_B2.Inventory.Service;
using Unity;

namespace OSL_B2.Inventory.Web
{
    public static class Factory
    {
        public static T GetService<T>()
        {
            var container = new UnityContainer();
            WebModule.Register(container);
            ServiceModule.Register(container);
            return container.Resolve<T>();
        }
    }
}