using AutoMapper;
using OSL_B2.Inventory.Service.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace OSL_B2.Inventory.Web.Areas.Admin.Models
{
    public class SupplierCreateViewModel
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Customer name must be less then 50 charecter.")]
        [Display(Name = "Supplier Name")]
        public string Name { get; set; }

        [Required]
        [Phone]
        [MaxLength(15, ErrorMessage = "Mobile number must be less then 15 charecter.")]
        [Display(Name = "Mobile Number")]
        public string Mobile { get; set; }

        [MaxLength(256, ErrorMessage = "Address must be less then 256 charecter.")]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [MaxLength(256, ErrorMessage = "Details must be less then 256 charecter.")]
        [Display(Name = "Details")]
        public string Details { get; set; }

        public SupplierDto GetSupplier(long id)
        {
            var supplier = Mapper.Map<SupplierDto>(this);
            supplier.CreatedBy = supplier.ModifiedBy = id;
            supplier.CreatedDate = supplier.ModifiedDate = DateTime.Now;
            return supplier;
        }
    }

    public class SupplierEditViewModel
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Customer name must be less then 50 charecter.")]
        [Display(Name = "Supplier Name")]
        public string Name { get; set; }

        [Required]
        [Phone]
        [MaxLength(15, ErrorMessage = "Mobile number must be less then 15 charecter.")]
        [Display(Name = "Mobile Number")]
        public string Mobile { get; set; }

        [MaxLength(256, ErrorMessage = "Address must be less then 256 charecter.")]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [MaxLength(256, ErrorMessage = "Details must be less then 256 charecter.")]
        [Display(Name = "Details")]
        public string Details { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public SupplierDto GetSupplier(long id)
        {
            var supplier = Mapper.Map<SupplierDto>(this);
            supplier.ModifiedBy = id;
            supplier.ModifiedDate = DateTime.Now;
            return supplier;
        }
    }

    public class SupplierDetailsViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string Details { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}