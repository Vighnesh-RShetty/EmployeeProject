using AutoMapper;
using EmployeeProject.Data;
using EmployeeProject.DataAccess.Models;
using EmployeeProject.Repository;
using EmployeeProject.Repository.Interfaces;
using static EmployeeProject.Data.CompanyModel;

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
        //This is for the generate the reset password

        public string GenerateResetToken(string email)
        {
            Company model = _companyRepository.GetByEmail(email);
            if (model == null)
            {
                return null;
            }
            else
            {
                //Generates any random strings
                string token = Guid.NewGuid().ToString();
                _companyRepository.updateResetToken(email, token);
                return token;
            }
        }
        public bool ResetPassword(string token, string newPassword)
        {
            var company = _companyRepository.GetCompanyByResetToken(token);
            if (company == null)
            {
                return false;
            }
            else
            {
                _companyRepository.updatePasswordByToken(token, newPassword);
                return true;
            }
        }
    }
}