using AutoMapper;
using OSL_B2.Inventory.Membership.DbContexts;
using OSL_B2.Inventory.Service.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OSL_B2.Inventory.Web.Areas.Admin.Models
{
    public class CategoryListViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }

    public class CategoryCreateViewModel
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Category name must be less then 50 charecter.")]
        [Display(Name = "Category Name")]
        public string Name { get; set; }

        public CategoryDto GetCategory(ApplicationUser appUser)
        {
            var category = Mapper.Map<CategoryDto>(this);
            category.CreatedBy = appUser.Id;
            category.CreatedDate = DateTime.Now;
            return category;
        }
    }
}