using Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository
{
    // Here is the abstract CRUD implementation
    // Note that generics are being using  for the context and also for the model
    public abstract class RepositoryBase<TContext,T> : IRepositoryBase<T> where T : class where TContext : DbContext
    {

        // ------------------------------------------------------------------------------
        // injection of the context
        protected TContext _context;
        public RepositoryBase(TContext context)
        {
            _context = context;
        }
        // ------------------------------------------------------------------------------

        public IQueryable<T> FindAll(bool trackChanges)
        {
            return !trackChanges ? _context.Set<T>().AsNoTracking() :
                _context.Set<T>();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
        {
            return !trackChanges ? _context.Set<T>().Where(expression).AsNoTracking() :
                _context.Set<T>();
        }

        public void Create(T entity)
        {
            _context.Set<T>().AddAsync(entity);
        }
        public void CreateRange(List<T> entity)
        {
            _context.Set<T>().AddRangeAsync(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

    }
}