using BackEnd.Repositories.Contracts;
using BackEnd.Repositories.EFCore;
using Microsoft.EntityFrameworkCore;
namespace BackEnd.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly ApplicationDbContext _context;

        public RepositoryBase(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Create(T entity)=>_context.Set<T>().Add(entity);

        public void Delete(T entity)=>_context.Set<T>().Remove(entity);

        public IQueryable<T> FindAll(bool trackChanges)
        {
            return !trackChanges ?
            _context.Set<T>()
            .AsNoTracking()
            : _context.Set<T>();
        }

        public IQueryable<T> FindByCondition(System.Linq.Expressions.Expression<Func<T, bool>> expression, bool trackChanges)
        {
            return !trackChanges ?
            _context.Set<T>().Where(expression).AsNoTracking(): _context.Set<T>().Where(expression);
        }

        public void Update(T entity)=>_context.Set<T>().Update(entity);

    }
}