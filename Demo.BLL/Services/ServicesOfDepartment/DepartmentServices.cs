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
    public class DepartmentServices(IUnitOfWork _unitOfWork) : IDepartmentServices
    {


        //Get All Departments
        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();
            return departments.Select(D => D.ToDepartmentDto());
        }

        //Get Department By Id
        public DepartmentDetailsDto? GetDepartmentById(int id)
        {
            var department = _unitOfWork.DepartmentRepository.GetById(id);
            return department?.ToDepartmentDetailsDto();
        }

        //Create New Department
        public int AddDepartment(CreatedDepartmentDto departmentDto)
        {
            var department = departmentDto.ToEntity();
            _unitOfWork.DepartmentRepository.Insert(department);
            return _unitOfWork.SaveChanges(); //Save to database
        }

        //Update Department
        public int UpdateDepartment(UpdatedDepartmentDto departmentDto)
        {
             _unitOfWork.DepartmentRepository.Update(departmentDto.ToEntity());
            return _unitOfWork.SaveChanges(); //Save to database
        }

        //Delete Department
        public bool RemoveDepartment(int id)
        {
            var department = _unitOfWork.DepartmentRepository.GetById(id);
            if (department == null) return false;
            else
            {
               _unitOfWork.DepartmentRepository.Remove(department);
                int result = _unitOfWork.SaveChanges(); //Save to database
                return result > 0 ? true : false;
            }
        }
    }
}
