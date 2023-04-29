namespace Edu_Store.Managers
{
    public class FolderManager
    {
        /// <summary>
        /// create a directory for the module
        /// </summary>
        /// <param name="TeacherUserName"></param>
        /// <param name="courseName"></param>
        /// <param name="DocumentName"></param>
        /// <param name="DocumentId"></param>
        /// <returns></returns>
        public static string CreateDirectory( string TeacherUserName , string courseName , string DocumentName , int DocumentId )
        {
            string FolderName = $"{DocumentId}-{DocumentName}";
            var directory = $"{Directory.GetCurrentDirectory( )}/wwwroot/Courses/{TeacherUserName}/{courseName}/{FolderName}";
            if ( !Directory.Exists( directory ) )

                Directory.CreateDirectory( directory );

            return directory;
        }
        /// <summary>
        /// create directory for the course
        /// </summary>
        /// <param name="TeacherUserName"></param>
        /// <param name="courseName"></param>
        /// <returns></returns>
        public static string CreateDirectory( string TeacherUserName , string courseName , int courseID )
        {

            var directory = $"{Directory.GetCurrentDirectory( )}/wwwroot/Courses/{TeacherUserName}/{courseID}-{courseName}";
            if ( !Directory.Exists( directory ) )

                Directory.CreateDirectory( directory );

            return directory;
        }
        /// <summary>
        /// deletes course and its modules
        /// </summary>
        /// <param name="TeacherUserName"></param>
        /// <param name="DirectoryName"></param>
        public static void DeleteDirectory( string TeacherUserName , string courseDirectory )
        {
            var directory = $"{Directory.GetCurrentDirectory( )}/wwwroot/Courses/{TeacherUserName}/{courseDirectory}";
            if ( Directory.Exists( directory ) )
            {
                // Delete all files from the Directory  
                foreach ( string filename in Directory.GetFiles( directory ) )
                {
                    File.Delete( filename );
                }
                // Check all child Directories and delete files  
                foreach ( string subfolder in Directory.GetDirectories( directory ) )
                {
                    DeleteDirectory( TeacherUserName , courseDirectory , subfolder );
                }
                Directory.Delete( directory );
            }
        }
        /// <summary>
        /// deletes a module
        /// </summary>
        /// <param name="TeacherUserName"></param>
        /// <param name="DirectoryName"></param>
        public static void DeleteDirectory( string TeacherUserName , string courseDirectory , string moduleDirectory )
        {
            var directory = $"{Directory.GetCurrentDirectory( )}/wwwroot/Courses/{TeacherUserName}/{courseDirectory}/{moduleDirectory}";
            if ( Directory.Exists( directory ) )
            {
                // Delete all files from the Directory  
                foreach ( string filename in Directory.GetFiles( directory ) )
                {
                    File.Delete( filename );
                }
                Directory.Delete( directory );
            }
        }
        public static void RenameExistingDirectory( string TeacherUserName , string courseDirectoryName , string oldDirectoryName , string newDirectoryName )
        {
            var directory = $"{Directory.GetCurrentDirectory( )}/wwwroot/Courses/{TeacherUserName}/{courseDirectoryName}";
            if ( Directory.Exists( $"{directory}/{oldDirectoryName}" ) )
            {
                Directory.Move( $"{directory}/{oldDirectoryName}" , $"{directory}/{newDirectoryName}" );
            }
        }
    }
}
