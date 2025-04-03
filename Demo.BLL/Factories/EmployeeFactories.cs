using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.BLL.DataTransferObjects.DepartmentDto;
using Demo.BLL.DataTransferObjects.EmployeeDto;
using Demo.DAL.Models.DepartmentModels;
using Demo.DAL.Models.EmployeeModels;
using Demo.DAL.Models.Shared.Enums;
using Demo.DAL.Repositories.Interfaces;

namespace Demo.BLL.Factories
{
    public static class EmployeeFactories
    {
        public static EmployeeDto ToEmployeeDto(this Employee E)
        {
            return new EmployeeDto
            {
                Id = E.Id,
                Name = E.Name,
                Age = E.Age,
                Salary = E.Salary,
                IsActive = E.IsActive,
                Email = E.Email,
                Gender = E.Gender.ToString(),
                EmployeeType = E.EmployeeType.ToString()
            };
        }

        public static EmployeeDetailsDto ToEmployeeDetailsDto(this Employee E)
        {
            return new EmployeeDetailsDto
            {
                Id = E.Id,
                Name = E.Name,
                Age = E.Age,
                Address = E.Address,
                Salary = E.Salary,
                IsActive = E.IsActive,
                Email = E.Email,
                PhoneNumber = E.Phone,
                Gender= E.Gender.ToString(),
                EmployeeType = E.EmployeeType.ToString(),
                HiringDate = DateOnly.FromDateTime(E.HiringDate),
                CreatedBy = 1,
                CreatedOn = (DateTime)E?.CreatedOn,
                LastModifiedBy = 1,
                LastModifiedOn = (DateTime)E.LastModifiedOn
            };
        }

        //public static Employee ToEntity(this CreateEmployeeDto employeeDto)
        //{
        //    return new Employee()
        //    {
        //        Name = employeeDto.Name,
        //        Age = employeeDto.Age,
        //        Address =  employeeDto.Address,
        //        Salary = employeeDto.Salary,
        //        IsActive = employeeDto.IsActive,
        //        Email = employeeDto.Email,
        //        Phone = employeeDto.Phone,
        //        Gender = employeeDto.Gender,
        //        EmployeeType = employeeDto.EmployeeType,
        //        HiringDate = employeeDto.HiringDate
        //    };
        //}

        //public static Employee ToEntity(this UpdateEmployeeDto updateEmployeeDto) => new Employee()
        //{
        //    Name = updateEmployeeDto.Name,
        //    Age = updateEmployeeDto.Age,
        //    Address = updateEmployeeDto.Address,
        //    Salary = updateEmployeeDto.Salary,
        //    IsActive = updateEmployeeDto.IsActive,
        //    Email = updateEmployeeDto.Email,
        //    Phone = updateEmployeeDto.Phone,
        //    Gender= updateEmployeeDto.Gender,
        //    EmployeeType = updateEmployeeDto.EmployeeType,
        //    HiringDate = updateEmployeeDto.HiringDate
        //};
    }
}
