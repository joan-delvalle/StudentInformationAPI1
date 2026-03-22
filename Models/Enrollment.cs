using System.ComponentModel.DataAnnotations;

namespace StudentInformationAPI1.Models
{
    public class Enrollment
    {
        public Guid EnrollmentId { get; set; }

        [Required]
        public Guid StudentId { get; set; }

        [Required]
        public Guid CourseId { get; set; }

        [Required]
        public string Semester { get; set; }

        [Range(1.0, 5.0)]
        public double Grade { get; set; }

        public Student Student { get; set; }
        public Course Course { get; set; }
    }
}
