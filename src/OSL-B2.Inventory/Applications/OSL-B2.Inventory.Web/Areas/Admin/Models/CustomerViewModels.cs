using AutoMapper;
using OSL_B2.Inventory.Service.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace OSL_B2.Inventory.Web.Areas.Admin.Models
{
    public class CustomerCreateViewModel
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Customer name must be less then 50 charecter.")]
        [Display(Name = "Customer Name")]
        public string Name { get; set; }

        [EmailAddress]
        [MaxLength(100, ErrorMessage = "Email must be less then 100 charecter.")]
        [Display(Name = "Customer Email")]
        public string Email { get; set; }

        [Required]
        [Phone]
        [MaxLength(15, ErrorMessage = "Mobile number must be less then 15 charecter.")]
        [Display(Name = "Customer Mobile")]
        public string Mobile { get; set; }

        [MaxLength(256, ErrorMessage = "Address must be less then 256 charecter.")]
        [Display(Name = "Customer Address")]
        public string Address { get; set; }

        internal CustomerDto GetCustomer(long id)
        {
            var customer = Mapper.Map<CustomerDto>(this);
            customer.CreatedBy = customer.ModifiedBy = id;
            customer.CreatedDate = customer.ModifiedDate = DateTime.Now;
            return customer;
        }
    }

    public class CustomerEditViewModel
    {
        public long Id { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Customer name must be less then 50 charecter.")]
        [Display(Name = "Customer Name")]
        public string Name { get; set; }

        [EmailAddress]
        [MaxLength(100, ErrorMessage = "Email must be less then 100 charecter.")]
        [Display(Name = "Customer Email")]
        public string Email { get; set; }

        [Required]
        [Phone]
        [MaxLength(15, ErrorMessage = "Mobile number must be less then 15 charecter.")]
        [Display(Name = "Customer Mobile")]
        public string Mobile { get; set; }

        [MaxLength(256, ErrorMessage = "Address must be less then 256 charecter.")]
        [Display(Name = "Customer Address")]
        public string Address { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public CustomerDto GetCustomer(long userId)
        {
            var customer = Mapper.Map<CustomerDto>(this);
            customer.ModifiedBy = userId;
            customer.ModifiedDate = DateTime.Now;
            return customer;
        }
    }

    public class CustomerDetailsViewModel
    {
        public long Id { get; set; }

        [Display(Name = "Customer Name")]
        public string Name { get; set; }

        [Display(Name = "Customer Email")]
        public string Email { get; set; }

        [Display(Name = "Customer Mobile")]
        public string Mobile { get; set; }

        [Display(Name = "Customer Address")]
        public string Address { get; set; }

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