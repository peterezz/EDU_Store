using LazZiya.ImageResize;
using System.Drawing;
namespace Edu_Store.Managers
{
    public class FileManager
    {
        public static string UploadFile( IFormFile file , string teacherUserName , string courseDirectory , string moduleDirectory )
        {
            var directory = $"{Directory.GetCurrentDirectory( )}/wwwroot/Courses/{teacherUserName}/{courseDirectory}/{moduleDirectory}";

            string FileName = $"{Guid.NewGuid( )}{Path.GetFileName( file.FileName )}";
            string finalpath = $"{directory}/{FileName}";
            using ( var stream = System.IO.File.Create( finalpath ) )
            {
                file.CopyTo( stream );
            }
            string recordedFinalPath = $"{teacherUserName}/{courseDirectory}/{moduleDirectory}/{FileName}";
            return recordedFinalPath;
        }
        public static string UploadPhoto( IFormFile PhotoFile , string PhysicalPath , int Width , int Height )
        {
            var img = Image.FromStream( PhotoFile.OpenReadStream( ) );
            var ScaleImage = ImageResize.Scale( img , Width , Height );
            int _min = 100;
            int _max = 999;
            Random _rdm = new Random( );
            int guid = _rdm.Next( _min , _max );
            string photopath = $"{Directory.GetCurrentDirectory( )}/wwwroot/{PhysicalPath}";
            string photoname = guid + Path.GetFileName( PhotoFile.FileName );
            string finalpath = photopath + photoname;
            SaveImage.SaveAs( ScaleImage , finalpath );

            return $"/{PhysicalPath}{photoname}";
        }
        public static void DeleteFile( string filePath )
        {
            string path = $"{Directory.GetCurrentDirectory( )}/wwwroot/{filePath}";

            try
            {
                // Check if file exists with its full path    
                if ( File.Exists( path ) )
                {
                    // If file found, delete it    
                    File.Delete( path );
                    Console.WriteLine( "File deleted." );
                }
                else Console.WriteLine( "File not found" );
            }
            catch ( IOException ioExp )
            {
                Console.WriteLine( ioExp.Message );
            }
        }
    }
}
