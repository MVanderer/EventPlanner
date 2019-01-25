using System;
using System.ComponentModel.DataAnnotations;

namespace BeltExam.Models
{
    public class ActivityDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext){
            DateTime startTime = Convert.ToDateTime(value);
            if (startTime>=DateTime.Now){
                return ValidationResult.Success;
            }
            else {
                return new ValidationResult(ErrorMessage);
            }
        }
    }
}