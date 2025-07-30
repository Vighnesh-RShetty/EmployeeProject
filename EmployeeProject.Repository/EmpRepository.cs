using EmployeeProject.DataAccess.Models;
using EmployeeProject.Models;
using EmployeeProject.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeProject.Repository
{
    public class EmpRepository : IEmpRepository
    {
        public readonly DataContext _dataContext;

        public EmpRepository()
        {
            _dataContext = new DataContext();
        }
        public Employee GetEmployee(int empId)
        {
            return _dataContext.Employees.Single(e => e.Id == empId);
        }
        public IEnumerable<Employee> GetEmployees(int companyId)
        {
            return _dataContext.Employees
                .Include(e => e.EmpDepartment)
                .Where(e => e.CompanyId == companyId).ToList();
        }
        void IEmpRepository.Add(Employee emp)
        {
            _dataContext.Employees.Add(emp);
        }
        void IEmpRepository.Update(Employee emp)
        {
            _dataContext.Entry(emp).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        void IEmpRepository.Delete(int id)
        {
            Employee emp = GetEmployee(id);
            _dataContext.Remove(emp);
        }
        void IEmpRepository.Save() => _dataContext.SaveChanges();

        public IEnumerable<EmployeeDepartment> GetEmployeeDepartments()
        {
            var sql = from ed in _dataContext.EmployeeDepartments
                      select ed;

            return sql;
        }
    
    }
}

