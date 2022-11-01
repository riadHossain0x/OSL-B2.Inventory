using AutoMapper;
using OSL_B2.Inventory.Service.Dtos;
using OSL_B2.Inventory.Web.Areas.Admin.Models;
using OSL_B2.Inventory.Service.Profiles;

namespace WholeSale.Web.Profiles
{
    public class WebProfile : Profile
    {
        public WebProfile()
        {
            Mapper.CreateMap<CategoryCreateViewModel, CategoryDto>().ReverseMap();
            Mapper.CreateMap<CategoryEditViewModel, CategoryDto>().ReverseMap();
            Mapper.CreateMap<CustomerCreateViewModel, CustomerDto>().ReverseMap();
            Mapper.CreateMap<CustomerEditViewModel, CustomerDto>().ReverseMap();
        }
    }
}