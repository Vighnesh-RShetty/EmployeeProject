using EmployeeProject.DataAccess.Models;
using EmployeeProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeProject.Repository.Interfaces
{
    public interface IEmpRepository
    {
        void Add(Employee emp);
        void Update(Employee emp);
        void Delete(int id);
        void Save();
        IEnumerable<Employee> GetEmployees(int companyId);
        Employee? GetEmployee(int id);
        IEnumerable<EmployeeDepartment> GetEmployeeDepartments();   
    }
}
