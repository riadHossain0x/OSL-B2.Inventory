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
    public class PurchaseCreateViewModel
    {
        [Required]
        [MaxLength(15, ErrorMessage = "Purchase No. must be less then 15 charecter.")]
        [Display(Name = "Purchase No")]
        public string PurchaseNo { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Purchase Date")]
        public DateTime PurchaseDate { get; set; }

        [MaxLength(256, ErrorMessage = "Purchase details must be less then 256 charecter.")]
        public string Details { get; set; }

        // Purchase Details
        [Required]
        //[ValidateEachItem]
        [Display(Name = "Supplier")]
        public List<long> SupplierId { get; set; }

        [Required]
       // [ValidateEachItem]
        [Display(Name = "Category")]
        public List<long> CategoryId { get; set; }

        [Required]
        //[ValidateEachItem]
        [Display(Name = "Product")]
        public List<long> ProductId { get; set; }

        [Required]
        [Display(Name = "Quantity")]
        public List<int> Quantity { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(19, 2)")]
        [Display(Name = "Buying Price")]
        public List<decimal> Price { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(19, 2)")]
        [Display(Name = "Sale Price")]
        public List<decimal> SalePrice { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(19, 2)")]
        [Display(Name = "Total")]
        public List<decimal> Total { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(19, 2)")]
        [Display(Name = "Grand Total")]
        public decimal GrandTotal { get; set; }
    }

    public class PurchaseDetailViewModel
    {
        public long Id { get; set; }

        [Display(Name = "Purchase No")]
        public string PurchaseNo { get; set; }

        [Display(Name = "Purchase Date")]
        public DateTime PurchaseDate { get; set; }

        [Display(Name = "Details")]
        public string Details { get; set; }

        [Display(Name = "Grand Total")]
        public decimal GrandTotal { get; set; }

        [Display(Name = "Modified By")]
        public long ModifiedBy { get; set; }

        [Display(Name = "Modified Date")]
        public DateTime ModifiedDate { get; set; }

        [Display(Name = "Created By")]
        public long CreatedBy { get; set; }

        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }
        public List<DetailsViewModel> PurchaseDetails { get; set; }

        public class DetailsViewModel
        {
            public long Id { get; set; }
            public long SupplierId { get; set; }
            public long ProductId { get; set; }
            public long PurchaseId { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }
            public decimal Total { get; set; }
        }
    }
}