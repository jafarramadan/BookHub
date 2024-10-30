using BlazorApp2.Client.Pages;
using BlazorApp2.Client.Services;
using BlazorApp2.Server.Repository;
using BlazorApp2.Shared;
using JLibrary.Server.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp2.Server.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly AuthorsContext _authorRepository; // Assume you have a repository pattern

        public AuthorController(AuthorsContext authorRepository)
        {
            _authorRepository = authorRepository;
        }

        [HttpGet]
        public IActionResult GetAllAuthors()

        {
            var allauthors = _authorRepository.Select().Execute();
            return Ok(allauthors);
        }
    }
}
