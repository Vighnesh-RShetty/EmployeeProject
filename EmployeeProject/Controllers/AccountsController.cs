using EmployeeProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using AspNetCoreGeneratedDocument;
using EmployeeProject.BusinessLogic;

namespace EmployeeProject.Controllers
{
    public class AccountsController : Controller
    {
        //private readonly DataContext _db;
        private readonly EmployeeManagement empLogic;

        public AccountsController()
        {
            empLogic=new EmployeeManagement();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            //var emp = _db.EmpTables.FirstOrDefault(e => e.Email == model.Email && e.MobileNumber == model.MobileNumber);
            var emp = empLogic.IsValidUser(model);
            if (emp != null)
            {
                    int Id = emp.Id;
                //HttpContext.Session.SetInt32("SessionId", Id);

                HttpContext.Session.SetString("email", emp.Email);
                HttpContext.Session.SetString("name", emp.Name);
                return RedirectToAction("Index", "Employee");
            }

            ViewBag.Error = "Invalid login credentials";
            return View(model);
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
