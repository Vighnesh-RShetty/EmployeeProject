using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeProject.Data
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public long MobileNumber { get; set; }
        public int? DepartmentId { get; set; }
        public string? ProfilePicturePath { get; set; }
        public int CompanyId { get; set; }
        public string DepartmentName => EmpDepartment?.DepartmentName ?? "No Department";

        //Why?
        public EmployeeDepartmentModel EmpDepartment { get; set; }
        public List<DropDownItem> EmployeeDepartments { get; set; }
        
        public class DropDownItem
        {
            public int Value { get; set; }
            public string Text { get; set; }
        }

        public class EmployeeDepartmentModel
        {
            public int? DepartmentId { get; set; }
            public string DepartmentName { get; set; }
        }
    }
}



