using BackEnd.Repositories.EFCore;
using BackEnd.Models;
using BackEnd.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using BackEnd.RequestFeatures;
using BackEnd.Extensions;

namespace BackEnd.Repositories
{
    public sealed class BookRepository:RepositoryBase<Book>,IBookRepository
    {
        public BookRepository(ApplicationDbContext context):base(context){}

        public void CreateOneBook(Book book)=>Create(book);

        public void DeleteOneBook(Book book)=>Delete(book);

        public async Task<PagedList<Book>> GetAllBooksAsync(BookParameters bookParameters,bool trackChanges)
        {
            var books=await FindAll(trackChanges)
            .FilterBooks(bookParameters.MinPrice,bookParameters.MaxPrice)
            .Search(bookParameters.SearchTerm)
            .Sort(bookParameters.OrderBy)
            .ToListAsync();
            return PagedList<Book>.ToPagedList(
                books,
                bookParameters.PageNumber,
                bookParameters.PageSize
            );
        }

        public async Task<Book> GetOneBookByIdAsync(int id, bool trackChanges)=>
            await FindByCondition(b=>b.Id.Equals(id),trackChanges).SingleOrDefaultAsync();

        public void UpdateOneBook(Book book)=>Update(book);

        

    }
}