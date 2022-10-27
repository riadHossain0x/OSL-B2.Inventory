using AutoMapper;
using OSL_B2.Inventory.Service.Dtos;
using OSL_B2.Inventory.Web.Areas.Admin.Models;
using OSL_B2.Inventory.Service.Profiles;

namespace WholeSale.Web.Profiles
{
    public class WebProfile : ServiceProfile
    {
        public WebProfile()
        {
            Mapper.CreateMap<CategoryCreateViewModel, CategoryDto>().ReverseMap();
        }
    }
}