using System.ComponentModel.DataAnnotations.Schema;

namespace Edu_Store.Models
{
    public class StudentCourse
    {
        [ForeignKey("Student")]
        public int StudentID { get; set; }
        //public Student Student { get; set; }

        [ForeignKey("Course")]
        public int CourseID { get; set; }
        //public Course Course { get; set; }
        public int? StudentGrade { get; set; }
        
        //no difference in migration file
    }
}
