using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.BLL.DataTransferObjects;
using Demo.DAL.Models.DepartmentModels;

namespace Demo.BLL.Factories
{
    static class DepartmentFactories
    {
        public static DepartmentDto ToDepartmentDto(this Department D)
        {
            return new DepartmentDto
            {
                Id = D.Id,
                Code = D.Code,
                Name = D.Name,
                Description = D.Description,
                DateOfCreation = DateOnly.FromDateTime((DateTime)D.CreatedOn)
            };

        }


        public static DepartmentDetailsDto ToDepartmentDetailsDto(this Department department)
        {
            return new DepartmentDetailsDto
            {
                ID = department.Id,
                CreatedBy = department.CreatedBy,
                IsDeleted = department.IsDeleted,
                LastModifiedBy = department.LastModifiedBy,
                LastModifiedOn = DateOnly.FromDateTime((DateTime)department.LastModifiedOn),
                CreatedOn = DateOnly.FromDateTime((DateTime)department.CreatedOn),
                Name = department.Name,
                Code = department.Code,
                Descreption = department.Description
            };
        }

        public static Department ToEntity(this CreatedDepartmentDto departmentDto)
        {
            return new Department()
            {
                Name = departmentDto.Name,
                Code = departmentDto.Code,
                CreatedOn = departmentDto.DateOfCreation.ToDateTime(new TimeOnly()),
                Description = departmentDto.Description,
            };
        }

        public static Department ToEntity(this UpdatedDepartmentDto departmentDto) => new Department()
        {
            Id = departmentDto.Id,
            Name = departmentDto.Name,
            Code = departmentDto.Code,
            CreatedOn = departmentDto.DateOfCreation.ToDateTime(new TimeOnly()),
            Description = departmentDto.Description,
        };

    }
}
