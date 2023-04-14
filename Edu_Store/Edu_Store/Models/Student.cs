using System.ComponentModel.DataAnnotations;

namespace Edu_Store.Models
{
    public enum Gender
    {
        Male, Female
    }
    public class Student
    {
        [Key] public int StudentID { get; set; }

        [Required(ErrorMessage ="Name is required")]
        [StringLength(50)]
        public required string Name { get; set; }
        
        [Required]
        [StringLength(50)]
        [DataType(DataType.EmailAddress,ErrorMessage ="Email is required")]
        public required string Email { get; set; }

        [Required]
        [StringLength(50)]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        [MaxLength(15)]
        public string? MobileNum { get; set; }

        [EnumDataType(typeof(Gender))]
        public Gender? Gender { get; set; }
    }
}
