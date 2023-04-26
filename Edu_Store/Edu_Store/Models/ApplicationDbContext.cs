using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Edu_Store.Models
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext( DbContextOptions<ApplicationDbContext> options )
            : base( options )
        {
        }

        protected override void OnModelCreating( ModelBuilder builder )
        {
            base.OnModelCreating( builder );
            builder.Entity<StudentCourse>( entity =>
            {
                entity.HasKey( s => new { s.StudentId , s.CourseID } );
            } );
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<CourseModule> Modules { get; set; }
        public DbSet<ModuleLecture> Lectures { get; set; }
    }
}