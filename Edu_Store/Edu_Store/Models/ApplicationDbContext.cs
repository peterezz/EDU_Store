using Microsoft.AspNetCore.Identity;
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

            builder.Entity<StudentCourse>( entity =>
            {
                entity.HasKey( s => new { s.StudentId , s.CourseID } );
            } );
            builder.Entity<IdentityRole>( ).HasData(
                new IdentityRole( )
                {
                    Id = Guid.NewGuid( ).ToString( ) ,
                    Name = "Student" ,
                    NormalizedName = "student" ,
                    ConcurrencyStamp = Guid.NewGuid( ).ToString( )
                }

                );

            builder.Entity<IdentityRole>( ).HasData(
                new IdentityRole( )
                {
                    Id = Guid.NewGuid( ).ToString( ) ,
                    Name = "Teacher" ,
                    NormalizedName = "teacher" ,
                    ConcurrencyStamp = Guid.NewGuid( ).ToString( )
                }

                );
            base.OnModelCreating( builder );
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<CourseModule> Modules { get; set; }
        public DbSet<ModuleLecture> Lectures { get; set; }
        public DbSet<Cart> Carts { get; set; }
    }
}