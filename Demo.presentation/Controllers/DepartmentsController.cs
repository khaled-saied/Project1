using Demo.BLL.DataTransferObjects;
using Demo.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace Demo.presentation.Controllers
{
    public class DepartmentsController(IDepartmentServices _departmentServices,
        ILogger<DepartmentsController> _logger,
        IWebHostEnvironment _webHostEnvironment) : Controller
    {
        public IActionResult Index()
        {
            var departments = _departmentServices.GetAllDepartments();
            return View(departments);
        }

        #region Create Department
        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(CreatedDepartmentDto departmentDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int result = _departmentServices.AddDepartment(departmentDto);
                    if (result > 0)
                        return RedirectToAction(nameof(Index));
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Department was not created");
                        //return View(departmentDto);
                    }
                }
                catch (Exception ex)
                {
                    //log Exception
                    if (_webHostEnvironment.IsDevelopment())
                    {
                        //1- Devolpment=> log error in console and return error message to user
                        ModelState.AddModelError(string.Empty, ex.Message);
                        //return View(departmentDto);
                    }
                    else
                    {
                        //2-Deployment=> log error in file or database and return  error view,
                        _logger.LogError(ex.Message);
                        //return View(departmentDto);
                    }
                }
            }
            return View(departmentDto);
        }

        #endregion

        #region Details
        public IActionResult Details(int? id)
        {
            if(!id.HasValue) return BadRequest();
            var department = _departmentServices.GetDepartmentById(id.Value);
            if (department == null) return NotFound();
            return View(department);
        }
        #endregion
    }
}
