using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Edu_Store.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Course")]
        public int CourseID { get; set; }


        [ForeignKey("Student")]
        [Column(TypeName = "nvarchar(450)")]
        public string? StudentId { get; set; }

        public ApplicationUser? Student { get; set; }
        public Course? Course { get; set; }


    }
}
