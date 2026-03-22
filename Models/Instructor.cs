using System.ComponentModel.DataAnnotations;

namespace StudentInformationAPI1.Models
{
    public class Instructor
    {
        public Guid InstructorId { get; set; }

        [Required]
        [StringLength(20)]
        public string EmployeeNo { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public Guid DepartmentId { get; set; }

        public Department Department { get; set; }
    }
}
