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
            var allbooks = _bookContext.Select().Execute();
            return Ok(allbooks);
        }
        // GET: api/book/getbookbyid/{id}
        [HttpGet("{id}")]
        public IActionResult GetBookById(int id)
        {
            var book = _bookContext.Select().Where(s => s.Eq(r => r.BookId,id)).Execute().FirstOrDefault();
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        // POST: api/books
        [HttpPost]
        public IActionResult AddBook([FromBody] BookModel book)
        {
           var newBook = _bookContext.Insert().WithFields(b => b.Exclude(s =>s.BookId)).Execute(book,returnNewRecord: true);
            return CreatedAtAction(nameof(GetAllBooks),new {Id=newBook.BookId},newBook);
        }

        // PUT: api/books/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] BookModel book)
        {
            if (book == null)
            {
                return BadRequest("Book data is null.");
            }

            
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

        // DELETE: api/books/{id}
        [HttpGet("{id}")]
        public IActionResult DeleteBook([FromRoute]int id)
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
    }
}
