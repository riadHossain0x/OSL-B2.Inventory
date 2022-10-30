using OSL_B2.Inventory.Repository.DbContexts;
using OSL_B2.Inventory.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace OSL_B2.Inventory.Service
{
    public static class ServiceModule
    {
        public static void Register(UnityContainer container)
        {
            container.RegisterType<ICategoryService, CategoryService>();
        }
    }
}
