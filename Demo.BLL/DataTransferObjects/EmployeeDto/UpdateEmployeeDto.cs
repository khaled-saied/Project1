using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Models.EmployeeModels;
using Demo.DAL.Models.Shared.Enums;

namespace Demo.BLL.DataTransferObjects.EmployeeDto
{
    public class UpdateEmployeeDto
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
        public int CreatedBy { get; set; } // User Id
        public int LastModifiedBy { get; set; } // User Id
    }
}
