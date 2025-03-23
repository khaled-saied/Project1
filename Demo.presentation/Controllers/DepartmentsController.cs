using Demo.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace Demo.presentation.Controllers
{
    public class DepartmentsController(IDepartmentServices _departmentServices) : Controller
    {
        public IActionResult Index()
        {
            var departments = _departmentServices.GetAllDepartments();
            return View(departments);
        }
    }
}
