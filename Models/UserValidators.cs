using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace BeltExam.Models {
    public class UserNameAttribute : ValidationAttribute {
        protected override ValidationResult IsValid (object value, ValidationContext validationContext) {
            string valStr = Convert.ToString (value);
            for (int i = 0; i < valStr.Length; i++) {
                if (Char.IsNumber (valStr, i)) {
                    return new ValidationResult (ErrorMessage);
                }
            }
            return ValidationResult.Success;
        }
    }

    public class PasswordCheckAttribute : ValidationAttribute {
        protected override ValidationResult IsValid (object value, ValidationContext validationContext) {
            string valStr = Convert.ToString (value);
            string regex = @"(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z])";
            var match = Regex.Match(valStr,regex,RegexOptions.IgnoreCase);
            if (match.Success){
                return ValidationResult.Success;
            }
            else {
                return new ValidationResult(ErrorMessage);
            }
        }
    }
}