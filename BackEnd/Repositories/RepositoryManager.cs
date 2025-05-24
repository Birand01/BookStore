using BackEnd.Repositories.EFCore;
using BackEnd.Repositories.Contracts;

namespace BackEnd.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly ApplicationDbContext _context;
        private readonly Lazy<IBookRepository> _bookRepository;

        public RepositoryManager(ApplicationDbContext context)
        {
            _context = context;
            _bookRepository = new Lazy<IBookRepository>(()=>new BookRepository(context));
        }

        public IBookRepository Book => _bookRepository.Value;

        public void Save()=>_context.SaveChanges();

    }
}