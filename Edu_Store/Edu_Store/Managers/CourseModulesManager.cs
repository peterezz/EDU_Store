using DataAccessLayer.Repository;
using Edu_Store.Models;

namespace Edu_Store.Managers
{
    public class CourseModulesManager
    {
        private readonly BaseRepo<CourseModule> _repo;

        public CourseModulesManager( ApplicationDbContext dbContext )
        {
            _repo = new BaseRepo<CourseModule>( dbContext );
        }
        public void CreateModule( string teacherUserName , string courseDirectoryName , CourseModule courseModule )
        {
            _repo.Add( courseModule );
            FolderManager.CreateDirectory( teacherUserName , courseDirectoryName , courseModule.ModuleName , courseModule.Id );
        }
        public void UpdateModule( string teacherUserName , CourseModule courseModule )
        {
            var oldData = _repo.Get( courseModule.Id );
            if ( oldData != null )
            {
                if ( oldData.ModuleDirectoryName != courseModule.ModuleDirectoryName )
                    FolderManager.RenameExistingDirectory( teacherUserName , courseModule.Course.DirectoryName , oldData.ModuleDirectoryName , courseModule.ModuleDirectoryName );
                oldData.ModuleName = courseModule.ModuleName;
                oldData.TotalHour = courseModule.TotalHour;
                oldData.Description = courseModule.Description;
                _repo.Edit( oldData );
            }
        }
        public void DeleteModule( string teacherUserName , CourseModule courseModule )
        {
            _repo.Delete( courseModule.Id );
            FolderManager.DeleteDirectory(
                teacherUserName ,
                courseModule.Course.DirectoryName ,
                courseModule.ModuleDirectoryName
                );
        }
        public List<CourseModule> GetAllCourseModules( int courseID )
            => _repo.GetMany( module => module.CourseId == courseID , module => module.Lectures ).ToList( );
        public CourseModule GetModuleByID( int moduleID ) =>
            _repo.GetOne( module => module.Id == moduleID , module => module.Lectures , moduleID => moduleID.Course );
    }
}
