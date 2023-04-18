using Edu_Store.Managers;
using Edu_Store.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.DependencyResolver;

namespace Edu_Store.Controllers
{
    public class TeacherController : Controller
    {
        public TeacherManager TeacherManager { get; }

        public TeacherController( TeacherManager teacherManager) {
            TeacherManager = teacherManager;
        }
        // GET: TeacherController
        public ActionResult Index()
        {
            return View(TeacherManager.GetAllTeachers());
        }

        // GET: TeacherController/Details/5
        public ActionResult MyProfile(int id)
        {
            //TeacherManager.GetTeacherById(id)
            return View(TeacherManager.GetTeacherById(id));
        }

        // GET: TeacherController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TeacherController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TeacherController/Edit/5
        public ActionResult UpdateProfile(int id)
        {
            return View(TeacherManager.GetTeacherById(id));
        }

        // POST: TeacherController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateProfile(int id, Teacher teacher)
        {
            try
            {

                TeacherManager.Edit(teacher);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TeacherController/Delete/5
        public ActionResult Delete(int id)
        {
            TeacherManager.DeleteTeacherAccount(id);
            return RedirectToAction(nameof(Index));
        }

        // POST: TeacherController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
