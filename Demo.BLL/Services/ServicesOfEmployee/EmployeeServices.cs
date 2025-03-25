
using Demo.BLL.DataTransferObjects.EmployeeDto;
using Demo.BLL.Factories;
using Demo.DAL.Repositories.Interfaces;

namespace Demo.BLL.Services.ServicesOfEmployee
{
    public class EmployeeServices(IEmployeeRepository employeeRepository) : IEmployeeServices
    {
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;

        //Get All Employees
        public IEnumerable<EmployeeDto> GetAllEmployees()
        {
            var employees = _employeeRepository.GetAll();
            return employees.Select(D => D.ToEmployeeDto());
        }

        //Get Employee By Id
        public EmployeeDetailsDto? GetEmployeeById(int id)
        {
            var employee = _employeeRepository.GetById(id);
            return employee?.ToEmployeeDetailsDto();
        }

        //Create New Department
        public int AddEmployee(CreateEmployeeDto employeeDto)
        {
            var employee = employeeDto.ToEntity();
            return _employeeRepository.Insert(employee);
        }

        //Update Employee
        public int UpdateEmployee(UpdateEmployeeDto employeeDto)
        {
            return _employeeRepository.Update(employeeDto.ToEntity());
        }

        //Delete Employee
        public bool RemoveEmployee(int id)
        {
            var employee = _employeeRepository.GetById(id);
            if (employee == null) return false;
            else
            {
                int result = _employeeRepository.Remove(employee);
                return result > 0 ? true : false;
            }
        }

    }
}
