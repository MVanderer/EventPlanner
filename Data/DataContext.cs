using BeltExam.Models;
using Microsoft.EntityFrameworkCore;

namespace BeltExam.Data
{
    public class DataContext : DbContext
    {
        public DataContext (DbContextOptions options) : base (options) { }
        public DbSet<User> Users{get;set;}
        public DbSet<Activity> Activities{get;set;}
        public DbSet<Participation> Participations{get;set;}

    }
}