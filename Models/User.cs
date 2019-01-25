using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeltExam.Models
{
    public class User
    {
        //Universal user fields
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage="You kinda need this")]
        [UserName(ErrorMessage="Your name cannot contain any numbers")]
        public string FirstName { get; set; }

        [Required(ErrorMessage="You do need a last name")]
        [UserName(ErrorMessage="Your name cannot contain any numbers")]
        public string LastName { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType (DataType.Password)]
        [MinLength (8, ErrorMessage = "Password's too short")]
        [PasswordCheck(ErrorMessage="Your password must have at least one letter, one number, and one special character")]
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        [NotMapped]
        [Compare ("Password", ErrorMessage = "Passwords do not match")]
        [DataType (DataType.Password)]
        [Display (Name = "Confirm Password")]
        public string Confirm { get; set; }

        public List<Activity> OwnedActivities {get;set;}
        public List<Participation> Participations{get;set;}
    }
}