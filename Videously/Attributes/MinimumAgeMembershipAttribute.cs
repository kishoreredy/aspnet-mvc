using System;
using System.ComponentModel.DataAnnotations;
using Videously.Dtos;
using Videously.Models;

namespace Videously.Attributes
{
    public class MinimumAgeMembershipAttribute:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (Customer) validationContext.ObjectInstance;
            if (customer.MembershipType.Id < 2)
            {
                return ValidationResult.Success;
            }

            if (customer.BirthDate.HasValue)
            {
                var age = DateTime.Today.Year - customer.BirthDate.Value.Year;
                return age >= 18
                    ? ValidationResult.Success
                    : new ValidationResult("Customer should be at least 18 years to obtain membership");
            }
            else
            {
                return new ValidationResult("Birth Date is required");
            }
        }
    }

    public class MinimumAgeMembershipDtoAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (CustomerDto)validationContext.ObjectInstance;
            if (customer.MembershipType.Id < 2)
            {
                return ValidationResult.Success;
            }

            if (customer.BirthDate.HasValue)
            {
                var age = DateTime.Today.Year - customer.BirthDate.Value.Year;
                return age >= 18
                    ? ValidationResult.Success
                    : new ValidationResult("Customer should be at least 18 years to obtain membership");
            }
            else
            {
                return new ValidationResult("Birth Date is required");
            }
        }
    }
}