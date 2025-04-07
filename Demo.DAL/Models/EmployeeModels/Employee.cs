using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Models.DepartmentModels;
using Demo.DAL.Models.Shared;
using Demo.DAL.Models.Shared.Enums;

namespace Demo.DAL.Models.EmployeeModels
{
    public class Employee : BaseEntity
    {
        public string Name { get; set; } = null!;
        public int Age { get; set; }
        public string? Address { get; set; } 
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }
        public DateTime HiringDate { get; set; }

        public int? DepartmentId { get; set; } //FK
        public virtual Department? Department { get; set; }
    }
}
