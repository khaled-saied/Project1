using Demo.BLL.DataTransferObjects;

namespace Demo.BLL.Services
{
    public interface IDepartmentServices
    {
        int AddDepartment(CreatedDepartmentDto departmentDto);
        IEnumerable<DepartmentDto> GetAllDepartments();
        DepartmentDetailsDto? GetDepartmentById(int id);
        bool RemoveDepartment(int id);
        int UpdateDepartment(UpdatedDepartmentDto departmentDto);
    }
}