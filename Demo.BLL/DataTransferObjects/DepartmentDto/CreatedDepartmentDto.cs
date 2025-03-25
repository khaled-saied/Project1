using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.DataTransferObjects.DepartmentDto
{
    public class CreatedDepartmentDto
    {
        [Required]
        [Range(100,int.MaxValue)]
        public string Code { get; set; } = string.Empty;
        [Required(ErrorMessage = "Invalid Name!!!")]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;

        public DateOnly DateOfCreation { get; set; }

    }
}
