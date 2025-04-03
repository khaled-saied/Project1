
using AutoMapper;
using Demo.BLL.DataTransferObjects.EmployeeDto;
using Demo.BLL.Factories;
using Demo.DAL.Models.EmployeeModels;
using Demo.DAL.Repositories.Interfaces;

namespace Demo.BLL.Services.ServicesOfEmployee
{
    public class EmployeeServices(IEmployeeRepository employeeRepository,IMapper _mapper) : IEmployeeServices
    {
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;

        //Get All Employees
        public IEnumerable<EmployeeDto> GetAllEmployees(bool WithTracking = false)
        {
            var employees = _employeeRepository.GetAll(WithTracking);
            //return employees.Select(D => D.ToEmployeeDto());

            // SRC =>Employee
            // Destination => EmployeeDto
            return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }

        //Get Employee By Id
        public EmployeeDetailsDto? GetEmployeeById(int id)
        {
            var employee = _employeeRepository.GetById(id);
            //return employee?.ToEmployeeDetailsDto();
            return employee is null ? null : _mapper.Map<Employee,EmployeeDetailsDto>(employee);
        }

        //Create New Department
        public int AddEmployee(CreateEmployeeDto employeeDto)
        {
            //var employee = employeeDto.ToEntity();
            //return _employeeRepository.Insert(employee);

            return _employeeRepository.Insert(_mapper.Map<CreateEmployeeDto, Employee>(employeeDto));
        }

        //Update Employee
        public int UpdateEmployee(UpdateEmployeeDto employeeDto)
        {
            //return _employeeRepository.Update(employeeDto.ToEntity());
            return _employeeRepository.Update(_mapper.Map<UpdateEmployeeDto, Employee>(employeeDto));
        }

        //Delete Employee
        public bool RemoveEmployee(int id)
        {
            var employee = _employeeRepository.GetById(id);
            if (employee == null) return false;
            else
            {
                employee.IsDeleted = true;
                return _employeeRepository.Update(employee) > 0 ? true : false;
            }
        }

    }
}
