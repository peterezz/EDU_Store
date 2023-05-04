using Edu_Store.Enums;
using Edu_Store.Managers;
using Edu_Store.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Edu_Store.Controllers
{
    [Authorize( Roles = nameof( Roles.Teacher ) )]
    public class TeacherDashboardController : Controller
    {
        private readonly CourseManager courseManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly CourseModulesManager courseModuleManager;
        private readonly ModuleLecturesManager moduleLecturesManager;

        public TeacherDashboardController( CourseManager courseManager , CourseModulesManager courseModuleManager , ModuleLecturesManager moduleLecturesManager , UserManager<ApplicationUser> userManager )
        {
            this.courseManager = courseManager;
            this.courseModuleManager = courseModuleManager;
            this.moduleLecturesManager = moduleLecturesManager;
            this.userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index( )
        {

            var data = courseManager.GetAllTeacherCourses( );
            return View( data );
        }

        #region course area
        [HttpGet]
        public IActionResult AddCourse( )
        {
            return View( );
        }
        [HttpPost]
        public async Task<IActionResult> AddCourse( Course course )
        {
            if ( ModelState.IsValid )
            {
                var user = await userManager.GetUserAsync( User );
                course.TeacherID = user.Id;
                courseManager.AddCourse( course , user.UserName );
                return RedirectToAction( "Index" );
            }
            return View( );
        }
        [HttpGet]
        public async Task<IActionResult> DeleteCourse( int id )
        {
            var user = await userManager.GetUserAsync( User );
            var data = courseManager.GetCourseById( id , user.Id );
            if ( data == null )
                return RedirectToAction( nameof( Index ) );
            return View( data );
        }
        [HttpPost]
        public async Task<IActionResult> DeleteCourse( Course course )
        {
            var user = await userManager.GetUserAsync( User );
            courseManager.DeleteCourse( course , user.UserName );
            return RedirectToAction( "Index" );

        }
        #endregion

        #region module area
        [HttpGet]
        public async Task<IActionResult> ModuleIndex( int id )
        {
            var user = await userManager.GetUserAsync( User );
            var data = courseModuleManager.GetAllCourseModules( id , user.Id );
            if ( data.Count == 0 )
                return RedirectToAction( nameof( Index ) );
            return View( data );
        }
        [HttpGet]
        public IActionResult CreateModule( int courseId )
        {
            ViewBag.CourseID = courseId;
            return View( );
        }
        [HttpPost]
        public async Task<IActionResult> CreateModule( CourseModule module )
        {
            if ( !ModelState.IsValid )
                return View( );
            var user = await userManager.GetUserAsync( User );
            var courseDirectoryName = $"{courseManager.GetCourseById( module.CourseId ).DirectoryName}";
            courseModuleManager.CreateModule( user.UserName , courseDirectoryName , module );
            return RedirectToAction( nameof( ModuleIndex ) , new { id = module.CourseId } );
        }
        [HttpGet]
        public async Task<IActionResult> DeleteModule( int moduleId , int courseID )
        {
            var user = await userManager.GetUserAsync( User );
            var mod = courseModuleManager.SearchModuleByCourseID( user.Id , moduleId , courseID );
            if ( mod == null )
                return RedirectToAction( nameof( ModuleIndex ) );
            return View( mod );
        }
        [HttpPost]
        public IActionResult DeleteModule( CourseModule module )
        {
            if ( !ModelState.IsValid )
                return View( );
            courseModuleManager.DeleteModule( User.Identity.Name , module );
            return RedirectToAction( nameof( ModuleIndex ) );
        }

        #endregion

        #region lecture area
        [HttpGet]
        public IActionResult LectureIndex( int moduleID , int courseID )
        {
            var data = courseModuleManager.GetAllModuleLectures( courseID , moduleID );
            ViewBag.courseID = courseID;
            ViewBag.moduleID = moduleID;
            return View( data );
        }
        [HttpGet]
        public async Task<IActionResult> CreateLecture( int courseID , int moduleID )
        {
            var user = await userManager.GetUserAsync( User );
            var mod = courseModuleManager.SearchModuleByCourseID( user.Id , moduleID , courseID );
            if ( mod == null )
                return RedirectToAction( nameof( ModuleIndex ) );
            ViewBag.moduleID = mod.Id;
            ViewBag.courseID = mod.CourseId;
            return View( );
        }
        [HttpPost]
        public async Task<IActionResult> CreateLecture( ModuleLecture lecture )
        {
            if ( !ModelState.IsValid )
                return View( );
            var user = await userManager.GetUserAsync( User );
            var module = courseModuleManager.GetModuleByID( lecture.ModuleID );
            moduleLecturesManager.addLecture( lecture , user.UserName , module.Course.DirectoryName , module.ModuleDirectoryName );
            return RedirectToAction( nameof( LectureIndex ) , new { moduleID = module.Id , courseID = module.CourseId } );
        }
        [HttpGet]
        public async Task<IActionResult> DeleteLecture( int courseID , int moduleID , int lectureID )
        {
            var user = await userManager.GetUserAsync( User );
            var lecture = moduleLecturesManager.GetModuleLecture( user.Id , courseID , moduleID , lectureID );
            if ( lecture == null )
                return RedirectToAction( nameof( ModuleIndex ) );
            ViewBag.moduleId = moduleID;
            return View( lecture );
        }
        [HttpPost]
        public IActionResult DeleteLecture( ModuleLecture moduleLecture )
        {
            moduleLecturesManager.DeleteLecture( moduleLecture );
            var module = courseModuleManager.GetModuleByID( moduleLecture.ModuleID );
            return RedirectToAction( nameof( LectureIndex ) , new { moduleID = module.Id , courseID = module.CourseId } );
        }
        #endregion

    }
}
