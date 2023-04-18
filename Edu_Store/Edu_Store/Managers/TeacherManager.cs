/*using DataAccessLayer.Repository;
using Edu_Store.Models;

namespace Edu_Store.Managers
{
  
    public class TeacherManager
    {
        public TeacherManager( ApplicationDbContext context)
        {
            baseRepo=new BaseRepo<Teacher>(context);
            Context = context;
        }
        public BaseRepo<Teacher> baseRepo { get; set; }

        public ApplicationDbContext Context { get; }
        public List<Teacher> GetAllTeachers()
        {
            return baseRepo.GetAll().ToList();
        }
        public Teacher GetTeacherById(int id)
        {
            return baseRepo.Get(id);
        }
        public void Edit(Teacher teacher)
        {
           

             baseRepo.Edit(teacher);
        }
        public void DeleteTeacherAccount(int id)
        {
            baseRepo.Delete(id);


        }

    }
}
*/