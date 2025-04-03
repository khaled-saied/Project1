using Demo.BLL.Services.ServicesOfEmployee;
using Microsoft.AspNetCore.Mvc;

namespace Demo.presentation.Controllers
{
    public class EmployeesController(IEmployeeServices _employeeServices) : Controller
    {
        public IActionResult Index()
        {
            var employees = _employeeServices.GetAllEmployees();
            return View(employees);
        }
    }
}
