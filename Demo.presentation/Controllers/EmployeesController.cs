using Demo.BLL.DataTransferObjects.EmployeeDto;
using Demo.BLL.Services.ServicesOfEmployee;
using Microsoft.AspNetCore.Mvc;

namespace Demo.presentation.Controllers
{
    public class EmployeesController(IEmployeeServices _employeeServices,
                                     IWebHostEnvironment _webHostEnvironment,
                                     ILogger<EmployeesController> _logger) : Controller
    {
        public IActionResult Index()
        {
            var employees = _employeeServices.GetAllEmployees();
            return View(employees);
        }

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateEmployeeDto employeeDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int Result = _employeeServices.AddEmployee(employeeDto);
                    if (Result > 0)
                        return RedirectToAction("Index");
                    else
                    {
                        ModelState.AddModelError("", "Failed to create employee");
                    }
                }
                catch (Exception ex)
                {
                    //ModelState.AddModelError("", ex.Message);
                    if (_webHostEnvironment.IsDevelopment())
                    {
                        //1- Devolpment=> log error in console and return error message to user
                        ModelState.AddModelError("", ex.Message);
                    }
                    else
                    {
                        //2-Deployment=> log error in file or database and return  error view,
                        _logger.LogError(ex.Message);

                    }
                }
            }
            return View(employeeDto);
        }

        #endregion
    }
}
