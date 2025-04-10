
using AutoMapper;
using Demo.BLL.DataTransferObjects.EmployeeDto;
using Demo.BLL.Factories;
using Demo.BLL.Services.AttachmentServices;
using Demo.DAL.Models.EmployeeModels;
using Demo.DAL.Repositories.Interfaces;

namespace Demo.BLL.Services.ServicesOfEmployee
{
    public class EmployeeServices(IUnitOfWork _unitOfWork,IMapper _mapper,
                                  IAttachmentServices _attachmentServices) : IEmployeeServices
    {

        //Get All Employees
        public IEnumerable<EmployeeDto> GetAllEmployees(string? EmployeeSearchName)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrWhiteSpace(EmployeeSearchName))
                employees = _unitOfWork.EmployeeRepository.GetAll();
            else
                employees = _unitOfWork.EmployeeRepository.GetAll().Where(e => e.Name.ToLower().Contains(EmployeeSearchName.ToLower()));

            var employeeDto = _mapper.Map<IEnumerable<Employee>,IEnumerable<EmployeeDto>>(employees);
            //return employees.Select(D => D.ToEmployeeDto());

            // SRC =>Employee
            // Destination => EmployeeDto
            return employeeDto;
        }

        //Get Employee By Id
        public EmployeeDetailsDto? GetEmployeeById(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.GetById(id);
            //return employee?.ToEmployeeDetailsDto();
            return employee is null ? null : _mapper.Map<Employee,EmployeeDetailsDto>(employee);
        }

        //Create New Department
        public int AddEmployee(CreateEmployeeDto employeeDto)
        {
            //var employee = employeeDto.ToEntity();
            //return _employeeRepository.Insert(employee);

           _unitOfWork.EmployeeRepository.Insert(_mapper.Map<CreateEmployeeDto, Employee>(employeeDto)); //Add loaclly
            return _unitOfWork.SaveChanges(); //Save to database
        }

        //Update Employee
        public int UpdateEmployee(UpdateEmployeeDto employeeDto)
        {
            //return _employeeRepository.Update(employeeDto.ToEntity());
             _unitOfWork.EmployeeRepository.Update(_mapper.Map<UpdateEmployeeDto, Employee>(employeeDto));
            return _unitOfWork.SaveChanges(); //Save to database
        }

        //Delete Employee
        public bool RemoveEmployee(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.GetById(id);
            if (employee == null) return false;
            else
            {
                employee.IsDeleted = true;
                _unitOfWork.EmployeeRepository.Update(employee);
                return _unitOfWork.SaveChanges() > 0 ? true : false;
            }
        }

       
    }
}
