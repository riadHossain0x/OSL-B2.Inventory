﻿using AutoMapper;
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
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Phone]
        [MaxLength(15, ErrorMessage = "Mobile number must be less then 15 charecter.")]
        [Display(Name = "Mobile Number")]
        public string Mobile { get; set; }

        [MaxLength(256, ErrorMessage = "Address must be less then 256 charecter.")]
        [Display(Name = "Address")]
        public string Address { get; set; }

        internal CustomerDto GetCustomer(long id)
        {
            var customer = Mapper.Map<CustomerDto>(this);
            customer.CreatedBy = id;
            customer.CreatedDate = DateTime.Now;
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
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Phone]
        [MaxLength(15, ErrorMessage = "Mobile number must be less then 15 charecter.")]
        [Display(Name = "Mobile Number")]
        public string Mobile { get; set; }

        [MaxLength(256, ErrorMessage = "Address must be less then 256 charecter.")]
        [Display(Name = "Address")]
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
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}