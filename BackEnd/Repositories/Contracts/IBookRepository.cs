using BackEnd.Models;
using BackEnd.RequestFeatures;

namespace BackEnd.Repositories.Contracts
{
    public interface IBookRepository:IRepositoryBase<Book>
    {
        Task<PagedList<Book>> GetAllBooksAsync(BookParameters bookParameters,bool trackChanges);
        Task<Book> GetOneBookByIdAsync(int id,bool trackChanges);
        void CreateOneBook(Book book);
        void UpdateOneBook(Book book);
        void DeleteOneBook(Book book);
        
    }
}