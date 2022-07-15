using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Proper.Request.Response.Models.Enums
{
    public class ValidEnumValueAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Type enumType = value.GetType();
            bool valid = Enum.IsDefined(enumType, value);
            if (!valid)
            {
                return new ValidationResult(String.Format("{0} is not a valid value for type {1}", value, enumType.Name));
            }
            return ValidationResult.Success;
        }
    }
}
