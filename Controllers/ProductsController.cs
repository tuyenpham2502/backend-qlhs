using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QlhsServer.Models;
using QlhsServer.Repositories;

namespace QlhsServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IBookRepository _bookRepo;

        public ProductsController(IBookRepository repo)
        {
            _bookRepo = repo;
        }

        [HttpGet]
        [Authorize(Roles = Helpers.AppRole.SuperAdmin)]
        public async Task<IActionResult> GetAllBooks()
        {
            try
            {
                return Ok(await _bookRepo.GetAllBooksAsync());
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await _bookRepo.GetBookAsync(id);
            return book == null ? NotFound() : Ok(book);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddNewBook(BookModel model)
        {
            try
            {
                var newBookId = await _bookRepo.AddBookAsync(model);
                var book = await _bookRepo.GetBookAsync(newBookId);
                return book == null ? NotFound() : Ok(book);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] BookModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }
            await _bookRepo.UpdateBookAsync(id, model);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteBook([FromRoute] int id)
        {
            await _bookRepo.DeleteBookAsync(id);
            return Ok();
        }
    }
}
