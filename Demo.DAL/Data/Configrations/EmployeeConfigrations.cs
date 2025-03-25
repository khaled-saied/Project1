using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Models.Shared.Enums;

namespace Demo.DAL.Data.Configrations
{
    class EmployeeConfigrations : BaseEntityConfigration<Employee> , IEntityTypeConfiguration<Employee>
    {
        public new void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(e => e.Name).HasColumnType("nvarchar(50)");
            builder.Property(e => e.Address).HasColumnType("nvarchar(150)");
            builder.Property(e => e.Salary).HasColumnType("decimal(10,2)");
            builder.Property(e => e.Gender).HasConversion(
                (genderOfEmp) => genderOfEmp.ToString(),
                (_gender) => (Gender)Enum.Parse(typeof(Gender), _gender));

            builder.Property(e => e.EmployeeType).HasConversion(
             (typeOfEmp) => typeOfEmp.ToString(),
             (type) => (EmployeeType)Enum.Parse(typeof(EmployeeType), type));

            base.Configure(builder);
        }
    }
}
