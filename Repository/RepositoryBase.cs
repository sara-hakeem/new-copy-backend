using Contracts.Repository;
using Entities.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        public RepositoryContext repositoryContext;
        public RepositoryBase(RepositoryContext repositoryContext)
        {
            this.repositoryContext = repositoryContext;
        }
        public void Attach(T entity)
        {
            repositoryContext.Set<T>().Attach(entity);
        }

        public void Create(T entity)
        {
            repositoryContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            repositoryContext.Set<T>().Remove(entity);
        }

        public IQueryable<T> FindAll()
        {
            return repositoryContext.Set<T>();
        }

        public IQueryable<T> FindAllByCondition(Expression<Func<T,bool>> expression)
        {
            return repositoryContext.Set<T>().Where(expression);
        }

        public void Update(T entity)
        {
            repositoryContext.Set<T>().Update(entity);
        }
    }
}
