using BackEnd.DTO;
using BackEnd.Models;

namespace BackEnd.Services.Contracts
{
    public interface IBookService
    {
         IEnumerable<Book> GetAllBooks(bool trackChanges);
         Book GetOneBookById(int id,bool trackChanges);
         Book CreateOneBook(Book book);
         void UpdateOneBook(int id, BookDtoForUpdate bookDtoForUpdate,bool trackChanges);
         void DeleteOneBook(int id,bool trackChanges);
    }
}