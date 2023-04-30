
using Edu_Store.Areas.Identity.Pages.Account;
using Edu_Store.Managers;
using Edu_Store.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Edu_Store.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ILogger<LogoutModel> loggersec;
        private readonly ILogger<HomeController> _logger;
        private readonly CourseManager courseManager;

        public HomeController(SignInManager<ApplicationUser> signInManager, ILogger<LogoutModel> loggersec, ILogger<HomeController> logger ,CourseManager courseManager)
        {
            this.signInManager = signInManager;
            this.loggersec = loggersec;
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
        [HttpGet]
        //public async Task<IActionResult> Logout()
        //{
        //    var user = HttpContext.User;

        //        await signInManager.SignOutAsync();


        //    return View("Index");
        //}
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            await signInManager.SignOutAsync();
            loggersec.LogInformation("User logged out.");
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                // This needs to be a redirect so that the browser performs a new
                // request and the identity for the user gets updated.
             return   LocalRedirect("/Identity/Account/Login");
            }
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