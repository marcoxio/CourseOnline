using Dominio;
using Microsoft.EntityFrameworkCore;

namespace Persistencia
{
    public class CoursesOnlineContext : DbContext
    {
        public CoursesOnlineContext(DbContextOptions options) : base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CourseInstructor>().HasKey(ci => new {ci.InstructorId,ci.CourseId});
        }

        //Representation of DB 
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<CourseInstructor> CourseInstructor { get; set; }
        public DbSet<Instructor> Instructor { get; set; }
        public DbSet<Price> Price { get; set; }
    }
}