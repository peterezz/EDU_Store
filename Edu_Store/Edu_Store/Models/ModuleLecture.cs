using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Edu_Store.Models
{
    [Table( "lectures" )]
    public class ModuleLecture
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey( "Module" )]
        public int ModuleID { get; set; }
        [Required( ErrorMessage = "field is required" )]
        [StringLength( 50 , ErrorMessage = "field reached maximum length" )]
        public string LectureName { get; set; } = string.Empty;
        [NotMapped]
        public IFormFile? videoFile { get; set; }
        public string LectureVedioPath { get; set; } = string.Empty;
        public CourseModule? Module { get; set; }
        [NotMapped]
        public string vedioPath => $"/Courses/{LectureVedioPath}";
        [Column( TypeName = "decimal(18,4)" )]
        public decimal LectureTime_MS { get; set; }



    }
}
