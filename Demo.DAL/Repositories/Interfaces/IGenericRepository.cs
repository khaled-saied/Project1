using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Models.DepartmentModels;


namespace Demo.DAL.Repositories.Interfaces
{
    public interface IGenericRepository <TEntity> where TEntity : BaseEntity
    {
        IEnumerable<TEntity> GetAll(bool withTracking = false);
        TEntity? GetById(int id);
        int Insert(TEntity entity);
        int Remove(TEntity entity);
        int Update(TEntity entity);
    }
}
