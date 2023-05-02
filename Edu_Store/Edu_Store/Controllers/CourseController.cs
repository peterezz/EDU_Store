﻿using Edu_Store.Managers;
using Edu_Store.Models;

using Microsoft.AspNetCore.Mvc;

namespace Edu_Store.Controllers
{

    //[Authorize]

    public class CourseController : Controller
    {
        // GET: CourseController
        public CourseManager CourseManager { get; }
        public CourseController( CourseManager courseManager )
        {
            CourseManager = courseManager;
        }

        public ActionResult Index( int? pageNumber )
        {

            int pagesize = 3;
            return View( Paginatedlist<Course>.create( CourseManager.GetAllCourses( ) , pageNumber ?? 1 , pagesize ) );

        }



        // GET: CourseController/Details/5
        public ActionResult Details( int id )
        {
            return View( CourseManager.GetCourseById( id ) );
        }

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
            //CourseManager.DeleteCourse( id );
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
