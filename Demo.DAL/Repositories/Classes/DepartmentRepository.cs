using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Models.DepartmentModels;
using Demo.DAL.Repositories.Interfaces;


namespace Demo.DAL.Repositories.Classes
{
    public class DepartmentRepository(ApplicationDbContext dbContext) : GenericRepository<Department>(dbContext),IDepartmentRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;  //2-> We are storing the injected ApplicationDbContext instance in a private field _dbContext.


        
    }
}
