using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.BLL.DataTransferObjects.DepartmentDto;
using Demo.BLL.Factories;
using Demo.DAL.Models;
using Demo.DAL.Repositories.Interfaces;

namespace Demo.BLL.Services.DepartmentsServices
{
    public class DepartmentServices(IDepartmentRepository departmentRepository) : IDepartmentServices
    {
        private readonly IDepartmentRepository _departmentRepository = departmentRepository;


        //Get All Departments
        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var departments = _departmentRepository.GetAll();
            return departments.Select(D => D.ToDepartmentDto());
        }

        //Get Department By Id
        public DepartmentDetailsDto? GetDepartmentById(int id)
        {
            var department = _departmentRepository.GetById(id);
            return department?.ToDepartmentDetailsDto();
        }

        //Create New Department
        public int AddDepartment(CreatedDepartmentDto departmentDto)
        {
            var department = departmentDto.ToEntity();
            return _departmentRepository.Insert(department);
        }

        //Update Department
        public int UpdateDepartment(UpdatedDepartmentDto departmentDto)
        {
            return _departmentRepository.Update(departmentDto.ToEntity());
        }

        //Delete Department
        public bool RemoveDepartment(int id)
        {
            var department = _departmentRepository.GetById(id);
            if (department == null) return false;
            else
            {
                int result = _departmentRepository.Remove(department);
                return result > 0 ? true : false;
            }
        }
    }
}
