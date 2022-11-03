using AutoMapper;
using OSL_B2.Inventory.Service.Dtos;
using System;
using System.ComponentModel.DataAnnotations;

namespace OSL_B2.Inventory.Web.Areas.Admin.Models
{
    public class CategoryCreateViewModel
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Category name must be less then 50 charecter.")]
        [Display(Name = "Category Name")]
        public string Name { get; set; }

        public CategoryDto GetCategory(long userId)
        {
            var category = Mapper.Map<CategoryDto>(this);
            category.CreatedBy = category.ModifiedBy = userId;
            category.CreatedDate = category.ModifiedDate = DateTime.Now;
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

    public class CategoryDetailsViewModel
    {
        public long Id { get; set; }

        [Display(Name = "Category Name")]
        public string Name { get; set; }

        [Display(Name = "Modified By")]
        public string ModifiedBy { get; set; }

        [Display(Name = "Modified Date")]
        public DateTime ModifiedDate { get; set; }

        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }
    }
}