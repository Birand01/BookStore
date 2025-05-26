using BackEnd.DTO;


namespace BackEnd.Services.Contracts
{
    public interface IBookService
    {
         Task<IEnumerable<BookDto>> GetAllBooksAsync(bool trackChanges);
         Task<BookDto> GetOneBookByIdAsync(int id,bool trackChanges);
         Task<BookDto> CreateOneBookAsync(BookDtoForInsertion bookDtoForCreation);
         Task UpdateOneBookAsync(int id, BookDtoForUpdate bookDtoForUpdate,bool trackChanges);
         Task DeleteOneBookAsync(int id,bool trackChanges);
    }

  

}