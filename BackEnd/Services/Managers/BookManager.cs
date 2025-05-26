using AutoMapper;
using BackEnd.DTO;
using BackEnd.Models;
using BackEnd.Repositories.Contracts;
using BackEnd.RequestFeatures;
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

        public async Task<BookDto> CreateOneBookAsync(BookDtoForInsertion bookDtoForCreation)
        {
            if(bookDtoForCreation is null)
            {
                throw new ArgumentNullException(nameof(bookDtoForCreation));
            }
            var entity=_mapper.Map<Book>(bookDtoForCreation);
            _manager.Book.CreateOneBook(entity);
            await _manager.SaveAsync();
            return _mapper.Map<BookDto>(entity);
        }

        public async Task DeleteOneBookAsync(int id, bool trackChanges)
        {
            var book=await _manager.Book.GetOneBookByIdAsync(id,trackChanges);
            if(book is null)
            {
                throw new Exception($"Book with id:{id} could not found.");
            }
            _manager.Book.DeleteOneBook(book);
            await _manager.SaveAsync();
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync(BookParameters bookParameters,bool trackChanges)
        {
            var books=await _manager.Book.GetAllBooksAsync(bookParameters,trackChanges);
            var booksDto=_mapper.Map<IEnumerable<BookDto>>(books);
            return booksDto;
        }

        public async Task<BookDto> GetOneBookByIdAsync(int id, bool trackChanges)
        {
            var book=await _manager.Book.GetOneBookByIdAsync(id,trackChanges);
            if(book is null)
            {
                throw new Exception($"Book with id:{id} could not found.");
            }
            return _mapper.Map<BookDto>(book);
        }

        public async Task UpdateOneBookAsync(int id, BookDtoForUpdate bookDtoForUpdate,bool trackChanges)
        {
            var entity=await _manager.Book.GetOneBookByIdAsync(id,trackChanges);
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
            await _manager.SaveAsync();
        }

    }
}