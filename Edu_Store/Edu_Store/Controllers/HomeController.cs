
using Edu_Store.Managers;
using Edu_Store.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Edu_Store.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CourseManager courseManager;

        public HomeController( ILogger<HomeController> logger ,CourseManager courseManager)
        {
            _logger = logger;
            this.courseManager = courseManager;
        }

        public IActionResult Index( )
        {
            return View( );
        }
        public ActionResult Contact()
        {
            return View();
        }


        public IActionResult Privacy( )
        {
            return View( );
        }

        [ResponseCache( Duration = 0 , Location = ResponseCacheLocation.None , NoStore = true )]
        public IActionResult Error( )
        {
            return View( new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier } );
        }
        public IActionResult About( )
        {
            return View( );
        }
        public IActionResult viewCourses(int? pageNumber)
        {
           // ViewBag.courseId = courseManager.GetStudentCourses();
            int pagesize = 3;
            return View(courseManager.GetStudentCourses());
        }

    }
}