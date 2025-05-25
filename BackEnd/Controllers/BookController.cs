using BackEnd.Repositories.EFCore;
using BackEnd.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;
using BackEnd.Repositories.Contracts;
using BackEnd.Services.Contracts;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IServiceManager _manager;
        private readonly ILoggerService _logger;

        public BookController(IServiceManager manager, ILoggerService logger)
        {
            _manager = manager;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            try
            {
                _logger.LogInfo("Getting all books");
                var books = _manager.BookService.GetAllBooks(false);
                return Ok(books);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetAllBooks: {ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult GetOneBook([FromRoute(Name = "id")] int id)
        {
            try
            {
                _logger.LogInfo($"Getting book with id: {id}");
                var book = _manager.BookService.GetOneBookById(id, false);
                if (book is null)
                {
                    _logger.LogWarn($"Book with id {id} not found");
                    return NotFound();
                }
                return Ok(book);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetOneBook: {ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateOneBook([FromBody] Book book)
        {
            try
            {
                _logger.LogInfo("Creating new book");
                if (book is null)
                {
                    _logger.LogWarn("Attempted to create null book");
                    return BadRequest();
                }
                _manager.BookService.CreateOneBook(book);
                _logger.LogInfo($"Book created successfully with id: {book.Id}");
                return StatusCode(201, book);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in CreateOneBook: {ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] Book book)
        {
            try
            {
                _logger.LogInfo($"Updating book with id: {id}");
                if(book is null)
                {
                    _logger.LogWarn("Attempted to update with null book");
                    return BadRequest();
                }
                _manager.BookService.UpdateOneBook(id, book, true);
                _logger.LogInfo($"Book with id {id} updated successfully");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in UpdateOneBook: {ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneBook([FromRoute(Name = "id")] int id)
        {
            try
            {
                _logger.LogInfo($"Deleting book with id: {id}");
                var entity = _manager.BookService.GetOneBookById(id, true);

                if (entity is null)
                {
                    _logger.LogWarn($"Book with id {id} not found for deletion");
                    return NotFound();
                }
                _manager.BookService.DeleteOneBook(id, true);
                _logger.LogInfo($"Book with id {id} deleted successfully");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in DeleteOneBook: {ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        [HttpPatch("{id:int}")]
        public IActionResult PartiallyUpdateOneBook([FromRoute(Name = "id")] int id,
        [FromBody] JsonPatchDocument<Book> bookPatch)
        {
            try
            {
                _logger.LogInfo($"Partially updating book with id: {id}");
                var entity = _manager.BookService.GetOneBookById(id, true);

                if (entity is null)
                {
                    _logger.LogWarn($"Book with id {id} not found for partial update");
                    return NotFound();
                }
                bookPatch.ApplyTo(entity);
                _manager.BookService.UpdateOneBook(id, entity, true);
                _logger.LogInfo($"Book with id {id} partially updated successfully");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in PartiallyUpdateOneBook: {ex.Message}");
                throw new Exception(ex.Message);
            }
        }
    }
}