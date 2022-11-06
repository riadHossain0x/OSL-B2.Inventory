﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OSL_B2.Inventory.Web.Attributes
{
    public class ValidateEachItemAttribute : ValidationAttribute
    {
        protected readonly List<ValidationResult> validationResults = new List<ValidationResult>();

        public override bool IsValid(object value)
        {
            var list = value as IEnumerable;
            if (list == null) return true;

            var isValid = true;

            foreach (var item in list)
            {
                var validationContext = new ValidationContext(item);
                var isItemValid = Validator.TryValidateObject(item, validationContext, validationResults, true);
                isValid &= isItemValid;
            }
            return isValid;
        }
    }
}