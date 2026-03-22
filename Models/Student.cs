using System.ComponentModel.DataAnnotations;

namespace StudentInformationAPI1.Models
{
    public class Student
    {
        public Guid StudentId { get; set; }

        [Required]
        [StringLength(20)]
        public int StudentNo { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }


    }
}
