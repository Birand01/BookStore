using BackEnd.Repositories.Contracts;
using BackEnd.Services.Contracts;

namespace BackEnd.Services.Managers
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IBookService> _bookService;

        public ServiceManager(IRepositoryManager manager)
        {
            _bookService = new Lazy<IBookService>(() => new BookManager(manager));
        }

        public IBookService BookService => _bookService.Value;
    }
}