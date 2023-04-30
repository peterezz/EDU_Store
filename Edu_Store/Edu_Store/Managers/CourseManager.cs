using DataAccessLayer.Repository;
using Edu_Store.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace Edu_Store.Managers
{
    public class CourseManager
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;

        public CourseManager( ApplicationDbContext context ,UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            baseRepo = new BaseRepo<Course>( context );
            st_Co_Repo = new BaseRepo<StudentCourse>(context );
            Context = context;
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
        }
        public BaseRepo<Course> baseRepo { get; set; }

        private BaseRepo<StudentCourse> st_Co_Repo;

        public ApplicationDbContext Context { get; }
        public List<Course> GetAllCourses( )
        {
            return baseRepo.GetAll( ).ToList( );
        }

        public List<Course> GetStudentCourses()
        {
            List<Course> std_courses = new List<Course>();
            string userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            
          var ldist=  st_Co_Repo.GetMany(c=>c.StudentId==userId).ToList();
            foreach(var course in ldist ) { 
               var cou= baseRepo.GetOne(c=>c.CourseID==course.CourseID);
                std_courses.Add(cou);
            }



            return std_courses;
        }
        public void AddCourse( Course course , string teacherUserName )
        {
            baseRepo.Add( course );
            FolderManager.CreateDirectory( teacherUserName , courseName: course.Title , courseID: course.CourseID );
        }
        public Course GetCourseById( int id )
        {
            return baseRepo.Get( id );
        }
        public void Edit( Course course )
        {


            baseRepo.Edit( course );
        }

        public void DeleteCourse( int id )
        {
            baseRepo.Delete( id );


        }

    }
}

