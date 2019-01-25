using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeltExam.Models
{
    public class Activity
    {
        [Key]
        public int ActivityId{get;set;}
        [Required]
        [Display (Name="Title")]
        [MinLength(2, ErrorMessage="Title's too short")]
        public string ActivityTitle{get;set;}
        [Required]
        [DataType(DataType.Date)]
        [Display(Name="Date")]
        [ActivityDate(ErrorMessage="Please time your activity so that it doesn't require time travel")]
        public DateTime StartDate{get;set;}

        [NotMapped]
        [DataType(DataType.Time)]
        [Display(Name="Time")]
        public DateTime StartTime{get;set;}

        [Required]
        [MinLength(10,ErrorMessage="Activity needs a more detailed description")]
        public string Description {get;set;}
        [Required]
        public int Duration {get;set;}
        [Required]
        public string TimeMeasure{get;set;}
        public int CoordinatorId{get;set;}
        public User Coordinator{get;set;}
        
        public List<Participation> Participations{get;set;}
        
    }
}