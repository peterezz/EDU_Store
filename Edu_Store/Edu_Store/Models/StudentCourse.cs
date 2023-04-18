using System.ComponentModel.DataAnnotations.Schema;

namespace Edu_Store.Models
{
    public class StudentCourse
    {
        [ForeignKey( "ApplicationUser" )]
        [Column( TypeName = "nvarchar(450)" )]
        public string StudentId { get; set; } = string.Empty;
        public ApplicationUser ApplicationUser { get; set; }

        [ForeignKey( "Course" )]
        public int? CourseID { get; set; }
        public Course? Course { get; set; }
        public int? StudentGrade { get; set; }

        //no difference in migration file
    }
}
