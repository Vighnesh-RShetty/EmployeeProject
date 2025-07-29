using AutoMapper;
using EmployeeProject.Data;
using EmployeeProject.DataAccess.Models;
using EmployeeProject.Models;
using EmployeeProject.Repository;
using EmployeeProject.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using static EmployeeProject.Data.CompanyModel;
using static EmployeeProject.Data.EmployeeModel;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EmployeeProject.BusinessLogic
{
    public class CompanyManagement
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CompanyManagement(IMapper mapper)
        {
            _companyRepository = new CompanyRepository();
            _mapper = mapper;
        }

        //This One Is To Create or Save The Company Details
        public string CreateCompanyDetail(CompanyModel model)
        {
            var Company = _companyRepository.GetByEmail(model.Email);
            if (Company == null)
            {
                Company dbCompany = _mapper.Map<Company>(model);

                _companyRepository.AddCompany(dbCompany);
                _companyRepository.Save();
            }
            else
            {
                return "EmailAlreadyExists";
            }
            return "Sucess";
        }

        //This Function Check the Validation at Time of Login(Email and Password)
        public Company ValidatePlainLogin(string email, string password)
        {

            var company = _companyRepository.GetByEmail(email);

            if (company != null && company.Password == password)
            {
                return company;
            }

            return null;
        }
        //This Method is used to get the Company using company id
        public CompanyModel GetCompany(int id)
        {
            Company company = _companyRepository.GetByCompanyId(id);

            return _mapper.Map<CompanyModel>(company);
        }

        //This Method is to handle the sector data
        public List<DropdownItem> GetCompanySectors()
        {
            var sql = from cs in _companyRepository.GetCompanySector()
                      select new DropdownItem
                      {
                          Text = cs.SectorName,
                          Value = cs.SectorId
                      };
            return sql.ToList();
        }

    }
}