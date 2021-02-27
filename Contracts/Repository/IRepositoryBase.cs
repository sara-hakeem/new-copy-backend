using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Contracts.Repository
{
    public interface IRepositoryBase<T>
    {
        void Create(T entity) ;
        void Update(T entity);
        void Attach(T entity);
        void Delete(T entity);
        IQueryable<T> FindAll();
        IQueryable<T> FindAllByCondition(Expression<Func<T,bool>> expression);
    }
}
