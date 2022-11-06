using OSL_B2.Inventory.Web.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OSL_B2.Inventory.Web.Areas.Admin.Models
{
    public class PurchaseCreateViewModels
    {
        [Required]
        [MaxLength(15, ErrorMessage = "Purchase No. must be less then 15 charecter.")]
        [Display(Name = "Purchase No")]
        public string PurchaseNo { get; set; }

        [Required]
        [Display(Name = "Purchase Date")]
        public DateTime PurchaseDate { get; set; }

        [MaxLength(256, ErrorMessage = "Purchase details must be less then 256 charecter.")]
        public string Details { get; set; }
        
        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(19, 2)")]
        [Display(Name = "Grand Total")]
        public decimal GrandTotal { get; set; }

        // Purchase Details
        [Required]
        //[ValidateEachItem]
        [Display(Name = "Supplier")]
        public List<long> SupplierId { get; set; }
        //public List<SelectListItem> Suppliers { get; set; }

        [Required]
       // [ValidateEachItem]
        [Display(Name = "Category")]
        public List<long> CategoryId { get; set; }
        //public List<SelectListItem> Categories { get; set; }

        [Required]
        //[ValidateEachItem]
        [Display(Name = "Product")]
        public List<long> ProductId { get; set; }
        //public List<SelectListItem> Products { get; set; }

        [Required]
        [Display(Name = "Quantity")]
        public List<int> Quantity { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(19, 2)")]
        [Display(Name = "Price")]
        public List<decimal> Price { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(19, 2)")]
        [Display(Name = "Total")]
        public List<decimal> Total { get; set; }
    }
}