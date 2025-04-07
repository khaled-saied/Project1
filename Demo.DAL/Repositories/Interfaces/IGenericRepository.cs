using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Models.DepartmentModels;


namespace Demo.DAL.Repositories.Interfaces
{
    public interface IGenericRepository <TEntity> where TEntity : BaseEntity
    {
        IEnumerable<TEntity> GetAll(bool withTracking = false);
        IEnumerable<TResult> GetAll<TResult>(Expression<Func<TEntity, TResult>> Selector);
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> Predicate);
        TEntity? GetById(int id);
        void Insert(TEntity entity);
        void Remove(TEntity entity);
        void Update(TEntity entity);
    }
}
