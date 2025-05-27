using BackEnd.Repositories.EFCore;
using BackEnd.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;
using BackEnd.Repositories.Contracts;
using BackEnd.Services.Contracts;
using BackEnd.DTO;
using BackEnd.ActionFilters;
using BackEnd.RequestFeatures;
using System.Text.Json;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ResponseCache(CacheProfileName = "300SecondsCache")]
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
        [ResponseCache(Duration = 60)] // Cache the response for 60 seconds
        public async Task<IActionResult> GetAllBooks([FromQuery] BookParameters bookParameters)
        {
            var pagedResult=await _manager.BookService.GetAllBooksAsync(bookParameters,false);
            Response.Headers.Add("X-Pagination",JsonSerializer.Serialize(pagedResult.metaData));
            return Ok(pagedResult.books);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOneBook([FromRoute(Name = "id")] int id)
        {
            try
            {
                _logger.LogInfo($"Getting book with id: {id}");
                var book = await _manager.BookService.GetOneBookByIdAsync(id, false);
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
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateOneBookAsync([FromBody] BookDtoForInsertion bookDtoForInsertion)
        {
            try
            {
                _logger.LogInfo("Creating new book");
                var bookDto=await _manager.BookService.CreateOneBookAsync(bookDtoForInsertion);
                return StatusCode(201, bookDto);
            }
            catch (Exception ex) 
            {
                _logger.LogError($"Error in CreateOneBook: {ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateOneBook([FromRoute(Name = "id")] int id, 
        [FromBody] BookDtoForUpdate bookDtoForUpdate)
        {
            try
            {
                _logger.LogInfo($"Updating book with id: {id}");
                if(bookDtoForUpdate is null)
                {
                    _logger.LogWarn("Attempted to update with null book");
                    return BadRequest();
                }
                await _manager.BookService.UpdateOneBookAsync(id, bookDtoForUpdate, true);
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
        public async Task<IActionResult> DeleteOneBook([FromRoute(Name = "id")] int id)
        {
            try
            {
                _logger.LogInfo($"Deleting book with id: {id}");
                var entity = await _manager.BookService.GetOneBookByIdAsync(id, true);

                if (entity is null)
                {
                    _logger.LogWarn($"Book with id {id} not found for deletion");
                    return NotFound();
                }
                await _manager.BookService.DeleteOneBookAsync(id, true);
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
        public async Task<IActionResult> PartiallyUpdateOneBook([FromRoute(Name = "id")] int id,
        [FromBody] JsonPatchDocument<BookDtoForUpdate> bookPatch)
        {
            try
            {
                _logger.LogInfo($"Partially updating book with id: {id}");
                var entity = await _manager.BookService.GetOneBookByIdAsync(id, true);

                if (entity is null)
                {
                    _logger.LogWarn($"Book with id {id} not found for partial update");
                    return NotFound();
                }

                var bookDtoForUpdate = new BookDtoForUpdate(entity.Id, entity.Title, entity.Price);
                bookPatch.ApplyTo(bookDtoForUpdate);
                await _manager.BookService.UpdateOneBookAsync(id, bookDtoForUpdate, true);
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