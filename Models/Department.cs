using System.ComponentModel.DataAnnotations;

namespace StudentInformationAPI1.Models
{
    public class Department
    {
        public Guid DepartmentId { get; set; }

        [Required]
        [StringLength(10)]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        public string Office { get; set; }
    }
}
