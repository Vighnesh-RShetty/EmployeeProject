using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeProject.Models
{
    [Table("EmpTable")]
    public class EmpTable
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age {  get; set; }
        public string Email { get; set; }
        public long MobileNumber { get; set; }

        public string? Department { get; set; } 
        public string? ProfilePicturePath { get; set; }

    }
}
