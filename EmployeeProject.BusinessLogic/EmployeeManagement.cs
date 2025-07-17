using EmployeeProject.Models;
using EmployeeProject.Repository;
using EmployeeProject.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeProject.BusinessLogic
{
    public class EmployeeManagement
    {
        private readonly IEmpRepository empRepository;

        public EmployeeManagement()
        {
            empRepository = new EmpRepository(new DataContext());

        }
        public IEnumerable<EmpTable> GetAllEmployee()
        {
            return empRepository.GetAll();
        }
        public EmpTable? GetEmployeeById(int id)
        {
            return empRepository.GetById(id);
        }
        public void AddEmployee(EmpTable empTable)
        {
            if (empTable.Age < 18)
            {
                throw new Exception("Age Must Be Greater Than 18");
            }
            empRepository.Add(empTable);
            empRepository.Save();
        }
        public void UpdateEmp(EmpTable empTable)
        {
            empRepository.Update(empTable);
            empRepository.Save();
        }
        public void DeleteEmp(int id)
        {
            empRepository.Delete(id);
            empRepository.Save();
        }
        public EmpTable? IsValidUser(LoginViewModel model)
        {
            return empRepository.GetByEmailAndMobile(model.Email,model.MobileNumber);
        }
    }
}