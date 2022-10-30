using AutoMapper;
using OSL_B2.Inventory.Service.Dtos;
using System;
using System.ComponentModel.DataAnnotations;

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

        public CategoryDto GetCategory(long userId)
        {
            var category = Mapper.Map<CategoryDto>(this);
            category.CreatedBy = userId;
            category.CreatedDate = DateTime.Now;
            return category;
        }
    }

    public class CategoryEditViewModel
    {
        public long Id { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Category name must be less then 50 charecter.")]
        [Display(Name = "Category Name")]
        public string Name { get; set; }
        [Required]
        public long CreatedBy { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }

        public CategoryDto GetCategory(long userId)
        {
            var category = Mapper.Map<CategoryDto>(this);
            category.ModifiedBy = userId;
            category.ModifiedDate = DateTime.Now;
            return category;
        }
    }
}