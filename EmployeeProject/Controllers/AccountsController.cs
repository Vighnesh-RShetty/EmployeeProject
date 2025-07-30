using EmployeeProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using AspNetCoreGeneratedDocument;
using EmployeeProject.BusinessLogic;
using AutoMapper;
using EmployeeProject.Data;

namespace EmployeeProject.Controllers
{
    public class AccountsController : Controller
    {
        private readonly CompanyManagement _companyLogic;

        public AccountsController(IMapper mapper)
        {
            _companyLogic = new CompanyManagement(mapper);
        }
        
        public IActionResult ForgotPassword()
        {
            return View();
        }

        public IActionResult GenerateToken(ForgotPasswordModel model)
        {
          var token = _companyLogic.GenerateResetToken(model.email);
            if (token == null)
            {
                ViewBag.Eror = "Emial Not Found";
                return View();
            }
            else
            {
                return RedirectToAction("ResetPassword", new { token = token });
            }
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult ResetPassword(string token)
        {
            var model = new ResetPasswordModel { Token = token };
            return View(model);
        }

        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordModel model)
        {
            bool sucess = _companyLogic.ResetPassword(model.Token, model.NewPassword);
            if (sucess)
            {
                return RedirectToAction("Login", "Company");
            }
            ViewBag.Error = "Invalid Token";
            return View(model);
        }
    }
}
