using AutoMapper;
using OSL_B2.Inventory.Entities.Entities;
using OSL_B2.Inventory.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSL_B2.Inventory.Service.Profiles
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            Mapper.CreateMap<CategoryDto, Category>().ReverseMap();
            Mapper.CreateMap<CustomerDto, Customer>().ReverseMap();
            Mapper.CreateMap<SupplierDto, Supplier>().ReverseMap();
        }
    }
}
