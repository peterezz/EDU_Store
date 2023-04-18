using DataAccessLayer.Repository;
using Edu_Store.Models;

namespace Edu_Store.Managers
{
    public class CourseManager
    {
        public CourseManager(ApplicationDbContext context)
        {
            baseRepo = new BaseRepo<Course>(context);
            Context = context;
        }
        public BaseRepo<Course> baseRepo { get; set; }

        public ApplicationDbContext Context { get; }
        public List<Course> GetAllCourses()
        {
            return baseRepo.GetAll().ToList();
        }
        public void AddCourse(Course course)
        {
            baseRepo.Add(course);
        }
        public Course GetCourseById(int id)
        {
            return baseRepo.Get(id);
        }
        public void Edit(Course course)
        {


            baseRepo.Edit(course);
        }
        public void DeleteCourse(int id)
        {
            baseRepo.Delete(id);


        }

    }
}

