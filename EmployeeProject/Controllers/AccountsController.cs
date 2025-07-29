using EmployeeProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using AspNetCoreGeneratedDocument;
using EmployeeProject.BusinessLogic;
using AutoMapper;

namespace EmployeeProject.Controllers
{
    public class AccountsController : Controller
    {
        private readonly CompanyManagement _companyLogic;

        public AccountsController(IMapper mapper)
        {
            _companyLogic = new CompanyManagement(mapper);
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
