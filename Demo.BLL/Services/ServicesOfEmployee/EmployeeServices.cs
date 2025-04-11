
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
            
            return employeeDto;
        }

        //Get Employee By Id
        public EmployeeDetailsDto? GetEmployeeById(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.GetById(id);
            var employeeDto = _mapper.Map<Employee, EmployeeDetailsDto>(employee);
            
            return employee is null ? null : employeeDto;
        }

        //Create New Department
        public int AddEmployee(CreateEmployeeDto employeeDto)
        {

            var employee = _mapper.Map<CreateEmployeeDto, Employee>(employeeDto);
            if(employeeDto.Image is not null)
            {
               employee.ImageName= _attachmentServices.UploadFile(employeeDto.Image, "Images");
            }

            _unitOfWork.EmployeeRepository.Insert(employee); //Add loaclly
            return _unitOfWork.SaveChanges(); //Save to database
        }

        //Update Employee
        //public int UpdateEmployee(UpdateEmployeeDto employeeDto)
        //{


        //    var employee = _mapper.Map<UpdateEmployeeDto, Employee>(employeeDto);
        //    bool flag;
        //    if (employee.ImageName is not null)
        //    {
        //        flag = _attachmentServices.DeleteFile(employee.ImageName);
        //        if (flag)
        //        {
        //        employee.ImageName = _attachmentServices.UploadFile(employeeDto.Image, "Images");
        //        }
        //    }

        //    _unitOfWork.EmployeeRepository.Update(employee);
        //    return _unitOfWork.SaveChanges(); //Save to database
        //}

        //Delete Employee

        public int UpdateEmployee(UpdateEmployeeDto employeeDto)
        {
            var existingEmployee = _unitOfWork.EmployeeRepository.GetById(employeeDto.Id);
            if (existingEmployee == null) return 0;

            _mapper.Map(employeeDto, existingEmployee);

            if (employeeDto.Image is not null)
            {
                if (!string.IsNullOrEmpty(existingEmployee.ImageName))
                {
                    _attachmentServices.DeleteFile(Path.Combine("wwwroot", "Files", "Images", existingEmployee.ImageName));
                }

                existingEmployee.ImageName = _attachmentServices.UploadFile(employeeDto.Image, "Images");
            }

            _unitOfWork.EmployeeRepository.Update(existingEmployee);
            return _unitOfWork.SaveChanges();
        }

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
