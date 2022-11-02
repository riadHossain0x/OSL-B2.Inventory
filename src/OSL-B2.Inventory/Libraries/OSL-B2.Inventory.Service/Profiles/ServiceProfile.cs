using AutoMapper;
using OSL_B2.Inventory.Entities.Entities;
using OSL_B2.Inventory.Service.Dtos;

namespace OSL_B2.Inventory.Service.Profiles
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            Mapper.CreateMap<CategoryDto, Category>().ReverseMap();
            Mapper.CreateMap<CustomerDto, Customer>().ReverseMap();
            Mapper.CreateMap<SupplierDto, Supplier>().ReverseMap();
            Mapper.CreateMap<ProductDto, Product>().ReverseMap();
        }
    }
}
