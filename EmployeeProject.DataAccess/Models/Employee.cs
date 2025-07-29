using EmployeeProject.DataAccess.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeProject.Models
{
    [Table("Employee")]
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age {  get; set; }
        public string Email { get; set; }
        public long MobileNumber { get; set; }
        public string? ProfilePicturePath { get; set; }
        [ForeignKey("Company")]
        public int CompanyId { get; set; }
        public Company Company { get; set; }


        [ForeignKey("EmpDepartment")]
        public int? DepartmentId { get; set; }
        public EmployeeDepartment EmpDepartment { get; set; }
    }
}

