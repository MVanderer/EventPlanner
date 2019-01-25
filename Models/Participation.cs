using System.ComponentModel.DataAnnotations;

namespace BeltExam.Models
{
    public class Participation
    {
        [Key]
        public int ParticipationId{get;set;}
        public int UserId{get;set;}
        public User User{get;set;}
        public int ActivityId{get;set;}
        public Activity Activity{get;set;}

    }
}