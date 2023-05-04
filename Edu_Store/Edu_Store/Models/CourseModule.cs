using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Edu_Store.Models
{
    [Table( "Modules" )]
    public class CourseModule
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey( "Course" )]
        public int CourseId { get; set; }

        [Required( ErrorMessage = "field is required" )]
        [StringLength( 50 , ErrorMessage = "field reached maximum length" )]
        public string ModuleName { get; set; } = string.Empty;

        [Required( ErrorMessage = "field is required" )]
        [Column( TypeName = "decimal(18,4)" )]
        public decimal TotalHour { get; set; }

        [Required( ErrorMessage = "field is required" )]
        [StringLength( 200 , ErrorMessage = "field reached maximum length" )]
        public string Description { get; set; } = string.Empty;
        public Course? Course { get; set; }
        public ICollection<ModuleLecture>? Lectures { get; set; }
        [NotMapped]
        public string ModuleDirectoryName { get { return $"{Id}-{ModuleName}"; } }
    }
}
