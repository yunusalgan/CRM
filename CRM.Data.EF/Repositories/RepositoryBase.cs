using CRM.Data.EF.Contexts;
using CRM.Data.Interfaces;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace CRM.Data.EF.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        public CrmContext _crmContext { get; set; }

        public RepositoryBase(CrmContext fxAdminDbContext)
        {
            _crmContext = fxAdminDbContext;
        }

        public int Insert(T entity)
        {
            _crmContext.Set<T>().Add(entity);

            return _crmContext.SaveChanges();
        }

        public int Update(T entity)
        {
            var existing = _crmContext.Set<T>().Local.ToList();

            foreach (T ent in existing)
            {
                _crmContext.Entry(ent).State = EntityState.Detached;
            }

            _crmContext.Entry(entity).State = EntityState.Modified;

            return _crmContext.SaveChanges();
        }

        public int Delete(T entity)
        {
            var existing = _crmContext.Set<T>().Local.ToList();

            foreach (T ent in existing)
            {
                _crmContext.Entry(ent).State = EntityState.Detached;
            }

            _crmContext.Entry(entity).State = EntityState.Deleted;

            return _crmContext.SaveChanges();
        }

        public int Delete(Expression<Func<T, bool>> expression)
        {
            _crmContext.Set<T>().RemoveRange(_crmContext.Set<T>().Where(expression));

            return _crmContext.SaveChanges();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return _crmContext.Set<T>().AsQueryable().Where(expression);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> expression)
        {
            var entity = _crmContext.Set<T>().Where(expression).FirstOrDefault();

            return entity;
        }

        public IEnumerable<T> GetAll()
        {
            return _crmContext.Set<T>().ToList();
        }
    }
}
