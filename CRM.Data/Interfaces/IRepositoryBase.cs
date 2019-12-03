using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CRM.Data.Interfaces
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);

        int Insert(T entity);

        int Update(T entity);

        int Delete(T entity);

        int Delete(Expression<Func<T, bool>> expression);

        T FirstOrDefault(Expression<Func<T, bool>> expression);

        IEnumerable<T> GetAll();
    }
}
