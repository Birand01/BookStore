using BackEnd.Data;
using BackEnd.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public BookController(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            try
            {
                var books = _context.Books.ToList();
                return Ok(books);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetOneBook([FromRoute(Name = "id")] Guid id)
        {
            try
            {
                var book = _context.Books.Where(b => b.Id.Equals(id)).SingleOrDefault();
                if (book is null)
                {
                    return NotFound(); // 404
                }
                return Ok(book);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateOneBook([FromBody] Book book)
        {
            try
            {
                if (book is null)
                {
                    return BadRequest(); // 400
                }
                _context.Books.Add(book);
                _context.SaveChanges();
                return StatusCode(201, book);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("{id:guid}")]
        public IActionResult UpdateOneBook([FromRoute(Name = "id")] Guid id, [FromBody] Book book)
        {
            try
            {
                var entity = _context.Books.Where(b => b.Id.Equals(id)).SingleOrDefault();
                if (entity is null)
                    return NotFound();

                if (id != book.Id)
                {
                    return BadRequest();
                }

                entity.Title = book.Title;
                entity.Author = book.Author;
                entity.ISBN = book.ISBN;
                entity.Price = book.Price;
                entity.PublicationDate = book.PublicationDate;
                entity.Description = book.Description;
                entity.PageCount = book.PageCount;
                entity.Language = book.Language;
                entity.Category = book.Category;
                entity.StockQuantity = book.StockQuantity;
                entity.CoverImageUrl = book.CoverImageUrl;
                entity.UpdatedAt = DateTime.UtcNow;

                _context.SaveChanges();
                return Ok(book);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteOneBook([FromRoute(Name = "id")] Guid id)
        {
            try
            {
                var entity = _context.Books.Where(b => b.Id.Equals(id)).SingleOrDefault();

                if (entity is null)
                {
                    return NotFound();
                }
                _context.Books.Remove(entity);
                _context.SaveChanges();
                return NoContent();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPatch("{id:guid}")]
        public IActionResult PartiallyUpdateOneBook([FromRoute(Name = "id")] Guid id,
        [FromBody] JsonPatchDocument<Book> bookPatch)
        {
            try
            {
                var entity = _context.Books.Where(b => b.Id.Equals(id)).SingleOrDefault();

                if (entity is null)
                {
                    return NotFound();
                }
                bookPatch.ApplyTo(entity);
                entity.UpdatedAt = DateTime.UtcNow;
                _context.SaveChanges();
                return NoContent();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}