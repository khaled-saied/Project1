using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.DataTransferObjects.DepartmentDto
{
    public class DepartmentDetailsDto
    {
        public int ID { get; set; } //Primary Key
        public int CreatedBy { get; set; } //User ID
        public DateOnly CreatedOn { get; set; }

        public int LastModifiedBy { get; set; }

        public DateOnly LastModifiedOn { get; set; }

        public bool IsDeleted { get; set; } // Soft Delete
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string? Descreption { get; set; }
    }
}
