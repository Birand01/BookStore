using BackEnd.Repositories.Contracts;

namespace BackEnd.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        public void Create(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> FindAll(bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> FindByCondition(System.Linq.Expressions.Expression<Func<T, bool>> expression, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

    }
}