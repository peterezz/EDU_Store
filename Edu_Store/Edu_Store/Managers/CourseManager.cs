using DataAccessLayer.Repository;
using Edu_Store.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;


namespace Edu_Store.Managers
{
    public class CourseManager
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;

        public CourseManager( ApplicationDbContext context , UserManager<ApplicationUser> userManager , IHttpContextAccessor httpContextAccessor )
        {
            baseRepo = new BaseRepo<Course>( context );
            st_Co_Repo = new BaseRepo<StudentCourse>( context );
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
        public List<Course> GetAllTeacherCourses( )
        {
            string userId = httpContextAccessor.HttpContext.User.FindFirstValue( ClaimTypes.NameIdentifier );
            return baseRepo.GetMany( course => course.TeacherID == userId ).ToList( );
        }


        public List<Course> GetStudentCourses( )
        {
            List<Course> std_courses = new List<Course>( );
            string userId = httpContextAccessor.HttpContext.User.FindFirstValue( ClaimTypes.NameIdentifier );

            var ldist = st_Co_Repo.GetMany( c => c.StudentId == userId ).ToList( );
            foreach ( var course in ldist )
            {
                var cou = baseRepo.GetOne( c => c.CourseID == course.CourseID );
                std_courses.Add( cou );
            }



            return std_courses;
        }
        public void AddCourse( Course course , string teacherUserName )
        {
            course.image = FileManager.UploadPhoto( course.ImageFile , $"images/" , 2500 , 1000 );
            baseRepo.Add( course );
            FolderManager.CreateDirectory( teacherUserName , courseName: course.Title , courseID: course.CourseID );
        }
        public Course GetCourseById( int id )
        {
            return baseRepo.Get( id );
        }
        public Course GetCourseById( int id , string teacherID )
        {
            return baseRepo.GetOne( course => course.CourseID == id && course.TeacherID.Equals( teacherID ) );
        }
        public void Edit( Course course )
        {


            baseRepo.Edit( course );
        }

        public void DeleteCourse( Course course , string TeacherUserName )
        {
            //baseRepo.Delete( course.CourseID );
            FolderManager.DeleteDirectory( TeacherUserName , course.DirectoryName );
        }
        public void GetCourses( List<StudentCourse> courses )
        {
            foreach ( var course in courses )
            {
                Context.StudentCourses.Add( course );
            }
            //Context.Carts.Remove(new Ca)
            Context.SaveChanges( );
        }
        public StudentCourse PreviewCourse( int courseID , string studentID )
        {
            //return 
            var data = Context.StudentCourses.Include( data => data.Course ).ThenInclude( data => data.Modules ).ThenInclude( data => data.Lectures ).FirstOrDefault( data => data.StudentId.Equals( studentID ) && data.CourseID == courseID );
            return data;
        }

        public ModuleLecture PreviewLecture( int lecID , int modID )
        {
            var data = Context.Lectures.FirstOrDefault( lec => lec.Id == lecID && lec.ModuleID == modID );
            return data;
        }
    }
}

