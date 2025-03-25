using Demo.BLL.DataTransferObjects.EmployeeDto;

namespace Demo.BLL.Services.ServicesOfEmployee
{
    public interface IEmployeeServices
    {
        int AddEmployee(CreateEmployeeDto employeeDto);
        IEnumerable<EmployeeDto> GetAllEmployees();
        EmployeeDetailsDto? GetEmployeeById(int id);
        bool RemoveEmployee(int id);
        int UpdateEmployee(UpdateEmployeeDto employeeDto);
    }
}