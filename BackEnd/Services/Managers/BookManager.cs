using AutoMapper;
using BackEnd.DTO;
using BackEnd.Models;
using BackEnd.Repositories.Contracts;
using BackEnd.Services.Contracts;

namespace BackEnd.Services.Managers
{
    public class BookManager : IBookService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        public BookManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
        }

        public Book CreateOneBook(Book book)
        {
            if(book is null)
            {
                throw new ArgumentNullException(nameof(book));
            }
            _manager.Book.CreateOneBook(book);
            _manager.Save();
            return book;
        }

        public void DeleteOneBook(int id, bool trackChanges)
        {
            var book=_manager.Book.GetOneBookById(id,trackChanges);
            if(book is null)
            {
                throw new Exception($"Book with id:{id} could not found.");
            }
            _manager.Book.DeleteOneBook(book);
            _manager.Save();
        }

        public IEnumerable<Book> GetAllBooks(bool trackChanges)
        {
            return _manager.Book.GetAllBooks(trackChanges);
        }

        public Book GetOneBookById(int id, bool trackChanges)
        {
            var book=_manager.Book.GetOneBookById(id,trackChanges);
            if(book is null)
            {
                throw new Exception($"Book with id:{id} could not found.");
            }
            return book;
        }

        public void UpdateOneBook(int id, BookDtoForUpdate bookDtoForUpdate,bool trackChanges)
        {
            var entity=_manager.Book.GetOneBookById(id,trackChanges);
            if(entity is null)
            {
                throw new Exception($"Book with id:{id} could not found.");
            }
            if(bookDtoForUpdate is null)
            {
                throw new ArgumentNullException(nameof(bookDtoForUpdate));
            }
            //map the dto to the entity
            _mapper.Map(bookDtoForUpdate,entity);
            _manager.Book.Update(entity);
            _manager.Save();
        }

    }
}