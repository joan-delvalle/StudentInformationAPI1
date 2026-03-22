using System.ComponentModel.DataAnnotations;

namespace StudentInformationAPI1.Models
{
    public class Course
    {
        public Guid CourseId { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string Title { get; set; }

        [Range(1, 6)]
        public int Units { get; set; }
    }
}
