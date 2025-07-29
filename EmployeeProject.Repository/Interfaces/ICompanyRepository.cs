using EmployeeProject.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeProject.Repository.Interfaces
{
    public interface ICompanyRepository
    {
        void AddCompany(Company company);
        void Save();
        Company GetByEmail(string email);
        Company GetByCompanyId(int compnayId);
        IEnumerable<CompanySector> GetCompanySector();
    }
}
