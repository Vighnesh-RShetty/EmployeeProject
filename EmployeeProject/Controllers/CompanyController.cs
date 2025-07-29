using AutoMapper;
using EmployeeProject.BusinessLogic;
using EmployeeProject.Data;
using EmployeeProject.DataAccess.Models;
using EmployeeProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace EmployeeProject.Controllers
{
    public class CompanyController : Controller
    {

        private readonly CompanyManagement _companyLogic;

        public CompanyController(IMapper mapper)
        {
            _companyLogic = new CompanyManagement(mapper);
        }

        //Return The View With Empty Model
        public IActionResult Index()
        {
            CompanyModel model = new CompanyModel();
            //model.EmployeeDepartments = empLogic.GetEmployeeDepartment();
            model.CompanySectors = _companyLogic.GetCompanySectors();
            return View(model);
        }

        // This Method Will Save The Company Details With The Image or Logo.
        [HttpPost]
        public IActionResult SaveCompany(CompanyModel model, IFormFile ProfilePicture)
        {

            if (ProfilePicture != null && ProfilePicture.Length > 0)
            {
                string wwwRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                if (!Directory.Exists(wwwRootPath))
                {
                    Directory.CreateDirectory(wwwRootPath);
                }

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(ProfilePicture.FileName);
                string fullPath = Path.Combine(wwwRootPath, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    ProfilePicture.CopyTo(stream);
                }

                model.CompanyLogoPath = "/uploads/" + fileName;
                Console.WriteLine(model.CompanyLogoPath);
            }
            string result = _companyLogic.CreateCompanyDetail(model);
            if (result == "EmailAlreadyExists")
            {
                ViewBag.ErrorMessage = "Email ID already exists. Please try another.";
                model.CompanySectors = _companyLogic.GetCompanySectors();
                return View("Index", model); // return same view with data
            }
            return View("~/Views/Accounts/Login.cshtml");
        }

        public IActionResult Login()
        {
            return View("~/Views/Accounts/Login.cshtml", new Company());
        }

        //This Method Will Validate The User Email and Password ..Which we receive from the ui and then passes it to the backend
        [HttpPost]
        public IActionResult ValidateLogin(CompanyModel model)
        {
            Company company = _companyLogic.ValidatePlainLogin(model.Email, model.Password);

            if (company != null)
            {
                HttpContext.Session.SetInt32("companyId", company.CompanyId);
                //Console.WriteLine(company.CompanyId);
                return RedirectToAction("HomePage", "Company");
            }
            ViewBag.ErrorMessage = "Invalid email or password";
            return View("~/Views/Accounts/Login.cshtml", model);
        }
        public IActionResult HomePage()
        {
            int? companyId = HttpContext.Session.GetInt32("companyId");

            if (companyId == null)
                return RedirectToAction("Login");

            return View();
        }
        //This Controller for DashBoard 
        public IActionResult Dashboard()
        {
            int? companyId = HttpContext.Session.GetInt32("companyId");
            if (!companyId.HasValue)
            {
                RedirectToAction("Login");
            }

            var company = _companyLogic.GetCompany((int)companyId);
            if (company == null)

                return RedirectToAction("Login");

            return View(company);

        }
        //The Logout Function 
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Company");
        }

    }
}

