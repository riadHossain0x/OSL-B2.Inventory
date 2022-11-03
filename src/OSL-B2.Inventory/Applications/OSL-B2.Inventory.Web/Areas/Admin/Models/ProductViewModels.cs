using AutoMapper;
using OSL_B2.Inventory.Service.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace OSL_B2.Inventory.Web.Areas.Admin.Models
{
    public class ProductCreateViewModel
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Product name must be less then 50 charecter.")]
        [Display(Name = "Product Name")]
        public string Name { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }

        [Display(Name = "Select Image")]
        public string Image { get; set; }

        [Required]
        [Display(Name = "Category")]
        public long CategoryId { get; set; }
        public List<SelectListItem> Categories { get; set; }

        [MaxLength(256, ErrorMessage = "Details must be less then 256 charecter.")]
        [Display(Name = "Product Details")]
        public string Details { get; set; }

        [Display(Name = "Critical Quantity")]
        public int Critical_Qty { get; set; }

        public ProductDto GetProduct(long id)
        {
            var product = Mapper.Map<ProductDto>(this);
            product.CreatedBy = product.ModifiedBy = id;
            product.CreatedDate = product.ModifiedDate = DateTime.Now;
            return product;
        }
    }

    public class ProductEditViewModel
    {
        public long Id { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Product name must be less then 50 charecter.")]
        [Display(Name = "Product Name")]
        public string Name { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }

        [Display(Name = "Select Image")]
        public string Image { get; set; }

        [Required]
        [Display(Name = "Category")]
        public long CategoryId { get; set; }
        public List<SelectListItem> Categories { get; set; }

        [MaxLength(256, ErrorMessage = "Details must be less then 256 charecter.")]
        [Display(Name = "Product Details")]
        public string Details { get; set; }

        [Display(Name = "Critical Quantity")]
        public int Critical_Qty { get; set; }
        public int Quantity { get; set; }
        public decimal BuyingPrice { get; set; }
        public decimal SalePrice { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public ProductDto GetProduct(long id)
        {
            var product = Mapper.Map<ProductDto>(this);
            product.ModifiedBy = id;
            product.ModifiedDate = DateTime.Now;
            return product;
        }
    }

    public class ProductDetailsViewModel
    {
        public long Id { get; set; }
        [Display(Name = "Product Name")]
        public string Name { get; set; }

        [Display(Name = "Image")]
        public string Image { get; set; }

        [Display(Name = "Category")]
        public string Category{ get; set; }

        [Display(Name = "Product Details")]
        public string Details { get; set; }

        [Display(Name = "Critical Quantity")]
        public int Critical_Qty { get; set; }

        [Display(Name = "Product Quantity")]
        public int Quantity { get; set; }

        [Display(Name = "Buying Price")]
        public decimal BuyingPrice { get; set; }

        [Display(Name = "Sale Price")]
        public decimal SalePrice { get; set; }

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