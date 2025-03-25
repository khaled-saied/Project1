using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Models.DepartmentModels;
using Demo.DAL.Repositories.Interfaces;

namespace Demo.DAL.Repositories.Classes
{
    public class EmployeeRepository(ApplicationDbContext dbContext) : GenericRepository<Employee>(dbContext), IEmployeeRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

       
    }
}
