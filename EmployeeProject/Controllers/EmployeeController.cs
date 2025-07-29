using AutoMapper;
using EmployeeProject.BusinessLogic;
using EmployeeProject.Data;
using EmployeeProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace EmployeeProject.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeManagement _empLogic;
        public EmployeeController(IMapper mapper)
        {

            _empLogic = new EmployeeManagement(mapper);
        }
        public IActionResult Index()
        {
            int? companyId = HttpContext.Session.GetInt32("companyId");
            if (companyId.HasValue)
            {
                var model = _empLogic.GetEmployees((int)companyId);
                return View(model);
            }
            else
            {
                return RedirectToAction("Login", "Company");
            }
        }

        [ActionName("add-new")]
        public IActionResult AddNew()
        {
            EmployeeModel model = new EmployeeModel();

            model.EmployeeDepartments = _empLogic.GetEmployeeDepartment();
            return View("~/Views/Employee/NewEmployee.cshtml", model);
        }

        //This will save the Employee Details
        [HttpPost]
        public IActionResult SaveEmployee(EmployeeModel model, IFormFile ProfilePicture)
        {
            int? companyId = HttpContext.Session.GetInt32("companyId");
            //
            if (companyId == null)
                return RedirectToAction("Login", "Accounts");
            //
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

            //Setting the Company Id
            model.CompanyId = companyId.Value;
            _empLogic.AddEmployee(model);

            return RedirectToAction("Index");
        }
        //This is for edit the emp details ,it will take the id and pass that to the Model
        public IActionResult Edit(int id)
        {
            EmployeeModel model = _empLogic.GetEmployee(id);
            model.EmployeeDepartments = _empLogic.GetEmployeeDepartment();

            if (model == null)
            {
                return NotFound();
            }
            return View("~/Views/Employee/EditEmployee.cshtml", model);
        }

        //Update the Details with image.
        [HttpPost]
        public IActionResult UpdateEmployee(EmployeeModel model, IFormFile ProfilePicture)
        {
            int? compnayId = HttpContext.Session.GetInt32("companyId");
            if (!compnayId.HasValue)
            {
                return RedirectToAction("Login", "Company");
            }

            Console.WriteLine(model);
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

                // Set new profile picture path
                model.ProfilePicturePath = "/uploads/" + fileName;
            }
            else
            {
                // Preserve existing image path if no new image is uploaded
                var existing = _empLogic.GetEmployee(model.Id);
                model.ProfilePicturePath = existing.ProfilePicturePath;
            }

            //Both Will Work
            model.CompanyId = (int)compnayId;
            model.CompanyId = compnayId.Value;
            _empLogic.UpdateEmp(model);

            return RedirectToAction("Index");
        }
        //This is an Delete Controller
        public IActionResult Delete(int id)
        {
            _empLogic.DeleteEmp(id);

            int? companyId = HttpContext.Session.GetInt32("companyId");
            if (companyId.HasValue)
            {
                var model = _empLogic.GetEmployees((int)companyId);
                return PartialView("~/Views/Employee/_Employee.cshtml", model);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        //This is for handle the CSV Feature
        [HttpGet]
        public IActionResult DownloadCsv()
        {
            int? companyId = HttpContext.Session.GetInt32("companyId");
            var employees = _empLogic.GetEmployees((int)companyId);

            var csv = new StringBuilder();
            csv.AppendLine("Name,Age,Email,Mobile,Department");

            foreach (var emp in employees)
            {
                var line = $"{emp.Name},{emp.Age},{emp.Email},{emp.MobileNumber},{emp.DepartmentName}";
                csv.AppendLine(line);
            }

            byte[] buffer = Encoding.UTF8.GetBytes(csv.ToString());

            return File(buffer, "text/csv", "EmployeeList.csv");
        }
    }
}

