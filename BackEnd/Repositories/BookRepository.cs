using BackEnd.Repositories.EFCore;
using BackEnd.Models;
using BackEnd.Repositories.Contracts;

namespace BackEnd.Repositories
{
    public class BookRepository:RepositoryBase<Book>,IBookRepository
    {
        public BookRepository(ApplicationDbContext context):base(context){}

        public void CreateOneBook(Book book)=>Create(book);

        public void DeleteOneBook(Book book)=>Delete(book);

        public IQueryable<Book> GetAllBooks(bool trackChanges)=>
        FindAll(trackChanges)
        .OrderBy(b=>b.Id);

        public IQueryable<Book> GetOneBookById(int id, bool trackChanges)=>
        FindByCondition(b=>b.Id.Equals(id),trackChanges);

        public void UpdateOneBook(Book book)=>Update(book);

        

    }
}