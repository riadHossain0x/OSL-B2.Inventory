﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace OSL_B2.Inventory.Web.Areas.Admin.Models
{
    public class SaleCreateViewModel
    {
        [Required]
        [Display(Name = "Customer Name")]
        public long CustomerId { get; set; }
        public List<SelectListItem> Customers { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Product")]
        public List<long> ProductId { get; set; }

        [Required]
        [Display(Name = "Quantity")]
        public List<int> Quantity { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Price")]
        public List<decimal> Price { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Total")]
        public List<decimal> Total { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Grand Total")]
        public decimal GrandTotal { get; set; }

        // Currently not fully implementated
        [Display(Name = "Paid Amount")]
        public decimal PaidAmount { get; set; }

        [Display(Name = "Due")]
        public decimal Due { get; set; }
    }
}