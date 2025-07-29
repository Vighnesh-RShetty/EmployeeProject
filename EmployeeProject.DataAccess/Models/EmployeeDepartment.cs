using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeProject.DataAccess.Models
{
    [Table("EmployeeDepartment")]
    public class EmployeeDepartment
    {
        [Key]
        public int? DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
}
