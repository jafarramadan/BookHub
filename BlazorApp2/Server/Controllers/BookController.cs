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
        // GET: api/book/getbookbyid/{id}
        [HttpGet("{id}")]
        public IActionResult GetBookById(int id)
        {
            try
            {
                var book = _bookContext.Select().Where(b => b.Eq(m => m.BookId, id)).Execute().FirstOrDefault();

                if (book == null)
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
                    .WithFields(a=> a.ExcludeAll().FromField(m=>m.Title).FromField(m=>m.Author).FromField(m => m.PublishedYear).FromField(m => m.Quantity))
                    .Execute(book);

                if (updateResult == null)
                {
                    return NotFound($"Book with id {id} not found.");
                }
                var updatedCourse = _bookContext.Select().Where(m => m.Eq(f => f.BookId, id)).Execute().FirstOrDefault();
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
