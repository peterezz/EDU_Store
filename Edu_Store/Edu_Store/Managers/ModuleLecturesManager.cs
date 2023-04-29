using DataAccessLayer.Repository;
using Edu_Store.Models;
using Microsoft.EntityFrameworkCore;

namespace Edu_Store.Managers
{

    //TODO : CRUD operation of module lectures
    public class ModuleLecturesManager
    {
        private readonly BaseRepo<ModuleLecture> _repo;
        private readonly ApplicationDbContext context;

        public ModuleLecturesManager( ApplicationDbContext context )
        {
            _repo = new BaseRepo<ModuleLecture>( context );
            this.context = context;
        }
        public void addLecture( ModuleLecture moduleLecture , string teacherUserName , string courseDirectoryName , string moduleDirectoryName )
        {
            moduleLecture.LectureVedioPath = FileManager.UploadFile( moduleLecture.videoFile , teacherUserName , courseDirectoryName , moduleDirectoryName );
            _repo.Add( moduleLecture );
        }
        public void DeleteLecture( ModuleLecture moduleLecture )
        {
            FileManager.DeleteFile( moduleLecture.vedioPath );
            _repo.Delete( moduleLecture.Id );
        }
        public void UpdateLectureData( ModuleLecture moduleLecture )
        {
            var oldData = _repo.Get( moduleLecture.Id );
            oldData.LectureName = moduleLecture.LectureName;
            _repo.Edit( oldData );

        }
        public void UpdateLectureVedio( ModuleLecture moduleLecture , string teacherUserName )
        {
            //upload vedio file
            moduleLecture.LectureVedioPath = FileManager.UploadFile( moduleLecture.videoFile , teacherUserName ,
                moduleLecture.Module.Course.DirectoryName ,
                moduleLecture.Module.ModuleDirectoryName );

            // delete old file
            var data = _repo.Get( moduleLecture.Id );
            FileManager.DeleteFile( data.vedioPath );

            // update data on database
            data.LectureVedioPath = moduleLecture.LectureVedioPath;
            data.LectureTime_MS = moduleLecture.LectureTime_MS;
            _repo.Edit( data );
        }
        public List<ModuleLecture> GetAllModuleLectures( )
            => _repo.GetAll( ).ToList( );

        public ModuleLecture GetModuleLecture( int lecID )
            => context.Lectures.Include( lec => lec.Module ).ThenInclude( module => module.Course ).FirstOrDefault( lec => lec.Id == lecID );

    }
}
