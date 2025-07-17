using EmployeeProject.BusinessLogic;
using EmployeeProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeProject.Controllers
{
    public class EmployeeController : Controller
    {
        //private readonly DataContext _database;
        private readonly EmployeeManagement empLogic;
        public EmployeeController()
        {
            //_database=new DataContext();
            empLogic= new EmployeeManagement();
        }
        public IActionResult Index()
        {
            var name = HttpContext.Session.GetString("name");
            
            ViewBag.msg = $"Welcome to LegalXGen  {name}";
            //Console.WriteLine(email);
            var model = empLogic.GetAllEmployee();

            return View(model);
        }

        [ActionName("add-new")]
        public IActionResult AddNew()
        {
            EmpTable model=new EmpTable();
            return View("~/Views/Employee/NewEmployee.cshtml",model);
        }

        /* public IActionResult SaveEmployee(EmpTable model)
         {
             _database.EmpTables.Add(model);
             _database.SaveChanges();
             return RedirectToAction("Index");
         }*/

        [HttpPost]
        public IActionResult SaveEmployee(EmpTable model, IFormFile ProfilePicture)
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

                model.ProfilePicturePath = "/uploads/" + fileName;
            }
            empLogic.AddEmployee(model);

            //var emps = empLogic.GetAllEmployee();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var employee=empLogic.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View("~/Views/Employee/EditEmployee.cshtml", employee);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult UpdateEmployee(EmpTable model) 
        {
            empLogic.UpdateEmp(model);  
                 
                return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            empLogic.DeleteEmp(id);
            var emps = empLogic.GetAllEmployee();
            return PartialView("~/Views/Employee/_Employee.cshtml", emps);
        }
    }
}
