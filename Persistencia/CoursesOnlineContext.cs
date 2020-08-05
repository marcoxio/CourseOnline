using Dominio.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistencia
{
    public class CoursesOnlineContext : IdentityDbContext<User>
    {
        public CoursesOnlineContext(DbContextOptions options) : base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
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