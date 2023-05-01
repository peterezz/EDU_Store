using Edu_Store.Managers;
using Edu_Store.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Edu_Store.Controllers
{
    // [Authorize( nameof( Roles.Teacher ) )]
    public class CourseController : Controller
    {
        private readonly ILogger<Course> logger;

        // GET: CourseController
        public CourseManager CourseManager { get; }
        public UserManager<ApplicationUser> UserManager { get; }
        public CartManager CartManager { get; }

        public CourseController( CourseManager courseManager , UserManager<ApplicationUser> _userManager , CartManager cartManager , ILogger<Course> logger )
        {
            CourseManager = courseManager;
            UserManager = _userManager;
            CartManager = cartManager;
            this.logger = logger;
        }

        public ActionResult Index( int? pageNumber )
        {

            int pagesize = 3;
            return View( Paginatedlist<Course>.create( CourseManager.GetAllCourses( ) , pageNumber ?? 1 , pagesize ) );

        }


        [HttpGet]
        // GET: CourseController/Details/5
        public ActionResult Details( int id )
        {
            return View( CourseManager.GetCourseById( id ) );
        }

        [HttpPost]
        public ActionResult AddToCart( int courseId )
        {
            logger.LogInformation( "" + courseId );
            var userID = UserManager.GetUserAsync( HttpContext.User ).Result?.Id;
            if ( string.IsNullOrEmpty( userID ) )
            {
                return RedirectToAction( "Index" );

            }
            else
            {
                CartManager.Add( new Cart { CourseID = courseId , StudentId = userID } );
                return RedirectToAction( "Index" , "Cart" ); //index getAll
            }
        }
        // Email ===> nancyyy@gmail.com   // nanno@gmail.com
        // Password ===> :gFzfQCb92SzyBp  // uVbH.BW24mv:-yZ

        // GET: CourseController/Create
        public ActionResult Create( )
        {
            return View( );
        }

        // POST: CourseController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Course crs )
        {
            try
            {

                CourseManager.AddCourse( crs , User.Identity.Name );
                return RedirectToAction( nameof( Index ) );
            }
            catch
            {
                return View( );
            }
        }

        // GET: CourseController/Edit/5
        public ActionResult Edit( int id )
        {
            return View( );
        }

        // POST: CourseController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( int id , IFormCollection collection )
        {
            try
            {
                return RedirectToAction( nameof( Index ) );
            }
            catch
            {
                return View( );
            }
        }

        // GET: CourseController/Delete/5
        public ActionResult Delete( int id )
        {
            CourseManager.DeleteCourse( id );
            return RedirectToAction( nameof( Index ) );
        }

        // POST: CourseController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete( int id , IFormCollection collection )
        {
            try
            {
                return RedirectToAction( nameof( Index ) );
            }
            catch
            {
                return View( );
            }
        }
    }
}
