using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Repositories.Interfaces;

namespace Demo.DAL.Repositories.Classes
{
    class EmployeeRepository(ApplicationDbContext dbContext) : GenericRepository<Employee>(dbContext) ,IGenericRepository<Employee>
    {
    }
}
