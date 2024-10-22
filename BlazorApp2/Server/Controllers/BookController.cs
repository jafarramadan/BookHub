using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using JLibrary.Server.Repository;
using BlazorApp2.Shared;

namespace JLibrary.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]/[Action]")]

    public class BookController : ControllerBase
    {
        private readonly BooksContext _bookContext;

        public BookController(BooksContext bookContext)
        {
            _bookContext = bookContext;
        }

        // GET: api/books
        [HttpGet]
        //[Route("ahmed")]
        public IActionResult GetAllBooks()
        {
            try
            {
                var books = _bookContext.Select().Execute();
                return Ok(books);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/books
        [HttpPost]
        public IActionResult AddBook([FromBody] BookModel book)
        {
            if (book == null)
            {
                return BadRequest("Book data is null.");
            }

            try
            {
                _bookContext.Insert().WithFields(b => b.Exclude(f => f.BookId)).Execute(book);
                return CreatedAtAction(nameof(GetAllBooks), new { id = book.BookId }, book);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/books/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] BookModel book)
        {
            if (book == null)
            {
                return BadRequest("Book data is null.");
            }

            try
            {
                var updateResult = _bookContext
                    .Update()
                    .Where(b => b.Eq(m => m.BookId, id))
                    .Execute(book);

                if (updateResult == null)
                {
                    return NotFound($"Book with id {id} not found.");
                }

                return Ok(book);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/books/{id}
        [HttpGet("{id}")]
        public IActionResult DeleteBook([FromRoute]int id)
        {
            try
            {
                var deleteResult = _bookContext
                    .Delete()
                    .Where(b => b.Eq(m => m.BookId, id))
                    .Execute();

                if (deleteResult == 0)
                {
                    return NotFound($"Book with id {id} not found.");
                }

                return Ok($"Book with id {id} deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
