using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryBase<T>
    {
        // ver cómo se aplicaría el principio SOLID ISP: https://anexsoft.com/solid-4-interface-segregation-principle-con-c
        // dividiendo en 3 interfaces:  IReadable, IWriteable, IRemovable  --> UserRepository : IReadable, IWriteable, IRemovable


        IQueryable<T> FindAll(bool trackChanges);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges);
        void Create(T entity);
        void CreateRange(List<T> entity);
        void Update(T entity);
        void Delete(T entity);
    }
}