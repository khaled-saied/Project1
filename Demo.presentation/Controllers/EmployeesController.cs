using Demo.BLL.DataTransferObjects.EmployeeDto;
using Demo.BLL.Services.DepartmentsServices;
using Demo.BLL.Services.ServicesOfEmployee;
using Demo.DAL.Models.EmployeeModels;
using Demo.DAL.Models.Shared.Enums;
using Demo.presentation.ViewModels.EmployeeViewMode;
using Microsoft.AspNetCore.Mvc;

namespace Demo.presentation.Controllers
{
    public class EmployeesController(IEmployeeServices _employeeServices,
                                     IWebHostEnvironment _webHostEnvironment,
                                     ILogger<EmployeesController> _logger ) : Controller
    {
        public IActionResult Index(string? EmployeeSearchName)
        {
            var employees = _employeeServices.GetAllEmployees(EmployeeSearchName);
            return View(employees);
        }

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var employeeDto = new CreateEmployeeDto()
                    {
                        Name = employeeViewModel.Name,
                        Age = employeeViewModel.Age,
                        Address = employeeViewModel.Address,
                        Salary = employeeViewModel.Salary,
                        IsActive = employeeViewModel.IsActive,
                        Email = employeeViewModel.Email,
                        PhoneNumber = employeeViewModel.PhoneNumber,
                        Gender = employeeViewModel.Gender,
                        EmployeeType = employeeViewModel.EmployeeType,
                        HiringDate = employeeViewModel.HiringDate,
                        DepartmentId = employeeViewModel.DepartmentId,
                        Image = employeeViewModel.Image
                    };

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
            return View(employeeViewModel);
        }

        #endregion

        #region Details
        public IActionResult Details(int? id)
        {
            if (!id.HasValue) return BadRequest();

            var employee = _employeeServices.GetEmployeeById(id.Value);
            if (employee == null)
                return NotFound();
            return View(employee);
        }
        #endregion

        #region Edit
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var employee = _employeeServices.GetEmployeeById(id.Value);
            if (employee == null) return NotFound();
            var employeeViewModel = new EmployeeViewModel()
            {
                Name = employee.Name,
                Age = employee.Age,
                Address = employee.Address,
                Salary = employee.Salary,
                IsActive = employee.IsActive,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                HiringDate = employee.HiringDate,
                Gender = Enum.Parse<Gender>(employee.Gender),
                EmployeeType = Enum.Parse<EmployeeType>(employee.EmployeeType),
                DepartmentId = employee.DepartmentId
            };
            return View(employeeViewModel);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int? id, EmployeeViewModel employeeViewModel)
        {
            if (!id.HasValue)
                return BadRequest();
            if (!ModelState.IsValid) return View(employeeViewModel);
            try
            {
                var employeeDto = new UpdateEmployeeDto()
                {
                    Id = id.Value,
                    Name = employeeViewModel.Name,
                    Age = employeeViewModel.Age,
                    Address = employeeViewModel.Address,
                    Salary = employeeViewModel.Salary,
                    IsActive = employeeViewModel.IsActive,
                    Email = employeeViewModel.Email,
                    PhoneNumber = employeeViewModel.PhoneNumber,
                    HiringDate = employeeViewModel.HiringDate,
                    Gender = employeeViewModel.Gender,
                    EmployeeType = employeeViewModel.EmployeeType,
                    DepartmentId = employeeViewModel.DepartmentId

                };

                int result = _employeeServices.UpdateEmployee(employeeDto);
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError("", "Failed to update employee");
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
                    return View("ErrorView", ex);
                }
            }
            return View(employeeViewModel);
        }
        #endregion

        #region Delete

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest();
            try
            {
                var result = _employeeServices.RemoveEmployee(id);
                if (result)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError("", "Failed to delete employee");
                    return RedirectToAction(nameof(Index) , new {id = id});
                }
            }
            catch (Exception ex)
            {
                //ModelState.AddModelError("", ex.Message);
                if (_webHostEnvironment.IsDevelopment())
                {
                    //1- Devolpment=> log error in console and return error message to user
                    //ModelState.AddModelError("", ex.Message);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    //2-Deployment=> log error in file or database and return  error view,
                    _logger.LogError(ex.Message);
                    return View("ErrorView", ex);
                }
            }
        }
        #endregion

    }
}
