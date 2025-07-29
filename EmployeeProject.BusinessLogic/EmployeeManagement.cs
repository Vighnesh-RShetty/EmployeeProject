using AutoMapper;
using EmployeeProject.Data;
using EmployeeProject.DataAccess.Models;
using EmployeeProject.Models;
using EmployeeProject.Repository;
using EmployeeProject.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EmployeeProject.Data.EmployeeModel;


namespace EmployeeProject.BusinessLogic
{
    public class EmployeeManagement
    {
        private readonly IEmpRepository _empRepository;
        private readonly IMapper _mapper;

        public EmployeeManagement(IMapper mapper)
        {
            _empRepository = new EmpRepository();
            _mapper = mapper;
        }
        //This method is to get the single emp record
        public EmployeeModel GetEmployee(int id)
        {
            Employee emp = _empRepository.GetEmployee(id);

            return _mapper.Map<EmployeeModel>(emp);
        }
        //This method is used to get the all the company details
        public List<EmployeeModel> GetEmployees(int companyId)
        {
            var employees = _empRepository
                .GetEmployees(companyId);

            return _mapper.Map<List<EmployeeModel>>(employees);
        }
        //This method is to add a new employee details
        public void AddEmployee(EmployeeModel model)
        {
            if (model.Age < 18)
            {
                throw new Exception("Age Must Be Greater Than 18");
            }

            Employee dbEmployee = _mapper.Map<Employee>(model);

            _empRepository.Add(dbEmployee);
            _empRepository.Save();
        }
        //This is to Update the Emp Details
        public void UpdateEmp(EmployeeModel model)
        {
            Employee emp = _empRepository.GetEmployee(model.Id);
            _mapper.Map(model, emp);

            _empRepository.Update(emp);
            _empRepository.Save();
        }
        public void DeleteEmp(int id)
        {
            _empRepository.Delete(id);
            _empRepository.Save();
        }
        //This method to handle the Employee Department
        public List<DropDownItem> GetEmployeeDepartment()
        {
            var sql = from ed in _empRepository.GetEmployeeDepartments()
                      select new DropDownItem
                      {
                          Text = ed.DepartmentName,
                          Value = (int)ed.DepartmentId
                      };
            return sql.ToList();
        }
    }
}