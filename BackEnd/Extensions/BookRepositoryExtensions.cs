
using BackEnd.Models;

namespace BackEnd.Extensions
{
    public static class BookRepositoryExtensions
    {
        public static IQueryable<Book> FilterBooks(this IQueryable<Book> books,
        uint minPrice,uint maxPrice)
        {
            return books.Where(book=>book.Price>=minPrice && book.Price<=maxPrice);
        }
        
    }
}
