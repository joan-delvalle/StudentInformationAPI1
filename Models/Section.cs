using System.ComponentModel.DataAnnotations;

namespace StudentInformationAPI1.Models
{
    public class Section
    {
        public Guid SectionId { get; set; }

        [Required]
        public string SectionCode { get; set; }

        [Required]
        public Guid CourseId { get; set; }

        [Required]
        public Guid InstructorId { get; set; }

        [Required]
        public string Room { get; set; }

        [Required]
        public string Schedule { get; set; }

        public Course Course { get; set; }
        public Instructor Instructor { get; set; }
    }
}
