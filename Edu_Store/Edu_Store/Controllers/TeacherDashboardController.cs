using Microsoft.AspNetCore.Mvc;

namespace Edu_Store.Controllers
{

    public class TeacherDashboardController : Controller
    {
        public IActionResult Index( )
        {
            return View( );
        }
    }
}
