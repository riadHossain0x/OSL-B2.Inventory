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

        [Display(Name = "Image")]
        public string ImagePath { get; set; }

        [Required]
        [Display(Name = "Category")]
        public long CategoryId { get; set; }
        public List<SelectListItem> Categories { get; set; }

        [MaxLength(256, ErrorMessage = "Details must be less then 256 charecter.")]
        [Display(Name = "Product Details")]
        public string Details { get; set; }
        public int CriticalQuantity { get; set; }

        
    }
}