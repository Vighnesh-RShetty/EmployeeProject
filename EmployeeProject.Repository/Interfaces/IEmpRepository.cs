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
        IEnumerable<EmpTable> GetAll();
        EmpTable? GetById(int id);
        void Add(EmpTable emp);
        void Update(EmpTable emp);
        void Delete(int id);
        void Save();
        /* EmpTable? IsValidUser(LoginViewModel loginViewModel);*/
        EmpTable? GetByEmailAndMobile(string email, long mobileNumber);
    }
}
