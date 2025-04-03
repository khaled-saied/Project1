using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Models.DepartmentModels;
using Demo.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Demo.DAL.Repositories.Classes
{
    public class GenericRepository<TEntity>(ApplicationDbContext dbContext) : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        //Get All
        public IEnumerable<TEntity> GetAll(bool withTracking = false)
        {
            if (withTracking)
            {
                return _dbContext.Set<TEntity>().Where(E=> E.IsDeleted != true).ToList();
            }
            return _dbContext.Set<TEntity>().Where(E => E.IsDeleted != true).AsNoTracking().ToList();
        }

        //Get By Id
        public TEntity? GetById(int id)
        {
            return _dbContext.Set<TEntity>().Find(id);
        }

        //Update
        public int Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            return _dbContext.SaveChanges();
        }

        //Delete
        public int Remove(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            return _dbContext.SaveChanges();
        }

        //Insert
        public int Insert(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
            return _dbContext.SaveChanges();
        }
    }
}
