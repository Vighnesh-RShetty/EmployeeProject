using AutoMapper;
using EmployeeProject.Data;
using EmployeeProject.DataAccess.Models;
using EmployeeProject.Models;

namespace EmployeeProject
{
    public class MappingProfile :Profile
    {
        public MappingProfile() { 
            CreateMap<CompanyModel,Company>().ReverseMap();
            CreateMap<EmployeeModel, Employee>().ReverseMap();
            CreateMap<EmployeeDepartment, EmployeeModel.EmployeeDepartmentModel>().ReverseMap();
            CreateMap<CompanySector,CompanyModel.CompanySectorModel>().ReverseMap();
           
        }
    }
}
