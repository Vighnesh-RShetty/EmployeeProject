using EmployeeProject.Models;
//using EmployeeProject.DataAccess;
using EmployeeProject.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeProject.Repository
{
    public class EmpRepository : IEmpRepository
    {
        public readonly DataContext _context;

        public EmpRepository(DataContext context)
        {
            _context = context;
        }

        public EmpTable? GetByEmailAndMobile(string email, long mobileNumber)
        {
            return _context.EmpTables.FirstOrDefault(e => e.Email == email && e.MobileNumber == mobileNumber);
        }
        /*public EmpTable? IsValidUser(LoginViewModel loginViewModel)
        {
            var sql = from EmpTable in _context.EmpTables
                      where EmpTable.Email == loginViewModel.Email && EmpTable.MobileNumber == loginViewModel.MobileNumber
                      select EmpTable;

            return sql.Any() ? sql.FirstOrDefault() : null;
        }*/

        void IEmpRepository.Add(EmpTable emp)
        {
           _context.Add(emp);
        }

        void IEmpRepository.Delete(int id)
        {
            var emp = _context.EmpTables.FirstOrDefault(e => e.Id == id);
            if (emp != null)
            {
                _context.Remove(emp);
            }
        }

        IEnumerable<EmpTable> IEmpRepository.GetAll()
        {
           return  _context.EmpTables.ToList();
        }

        EmpTable? IEmpRepository.GetById(int id)
        {
            return _context.EmpTables.FirstOrDefault(e=>e.Id==id);
        }

        void IEmpRepository.Save()=>_context.SaveChanges();

        //{
        //    _context.SaveChanges();
        //}

        void IEmpRepository.Update(EmpTable emp)
        {
            _context.EmpTables.Update(emp);
        }
    }
}
