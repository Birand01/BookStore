
using BackEnd.Data;
using BackEnd.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController:ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var books=ApplicationContext.Books;
            return Ok(books);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetOneBook([FromRoute(Name ="id")]int id)
        {
            var book=ApplicationContext.Books.Where(b=>b.Id.Equals(id)).SingleOrDefault();
            if(book is null)
            {return NotFound();} 
            return Ok(book);
        }

        [HttpPost]
        public IActionResult CreateOneBook([FromBody] Book book)
        {
            try
            {
                if(book is null)
                return BadRequest();

                ApplicationContext.Books.Add(book);
                return StatusCode(201,book);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    
        [HttpPut("{id:int}")]
        public IActionResult UpdateOneBook([FromRoute(Name="id")] int id,[FromBody] Book book )
        {
            var entity=ApplicationContext.Books.Find(b=>b.Id.Equals(id));
            if(entity is null)
            {
                return NotFound();//404
            }
            if(id!=book.Id)
            {
                return BadRequest();//400
            }
            ApplicationContext.Books.Remove(entity);
            book.Id=entity.Id;
            ApplicationContext.Books.Add(book);
            return Ok(book);
        }
    
        [HttpDelete]
        public IActionResult DeleteAllBooks()
        {
            ApplicationContext.Books.Clear();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneBook([FromRoute(Name ="id")]int id)
        {
            var entity=ApplicationContext.Books.Find(b=>b.Id.Equals(id));
            if(entity is null)
            {
                return NotFound();
            }
            ApplicationContext.Books.Remove(entity);
            return NoContent();
        }
   
        [HttpPatch("{id:int}")]
        public IActionResult PartiallyUpdateBook([FromRoute(Name="id")] int id,[FromBody] JsonPatchDocument<Book> book)
        {
             var entity=ApplicationContext.Books.Find(b=>b.Id.Equals(id));
            if(entity is null)
            {
                return NotFound();
            }
            book.ApplyTo(entity);
            return NoContent();
        }
    }
}