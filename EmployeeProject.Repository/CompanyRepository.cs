using EmployeeProject.DataAccess.Models;
using EmployeeProject.Repository.Interfaces;
using Microsoft.Data.Sql;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeProject.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DataContext _dataContext;

        public CompanyRepository()
        {
            _dataContext = new DataContext();
        }
        void ICompanyRepository.AddCompany(Company company)
        {

            _dataContext.Add(company);
        }
        void ICompanyRepository.Save()
        {
            _dataContext.SaveChanges();
        }
        public Company GetByEmail(string email)
        {
            return _dataContext.Companies.FirstOrDefault(t => t.Email == email);
        }

        public IEnumerable<CompanySector> GetCompanySector()
        {
            var sql = from cs in _dataContext.CompanySectors
                      select cs;

            return sql;
        }
        public Company GetByCompanyId(int compnayId)
        {
            return _dataContext.Companies
                .Include(c => c.CompanySector)
                .Single(c => c.CompanyId == compnayId);
        }
    }
}
