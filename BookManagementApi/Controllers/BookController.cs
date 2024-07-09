using BookManagementApi.Models;
using BookManagementApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BookManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly IBookRepository bookrepo;

        public BookController(IBookRepository bookrepo)
        {
            this.bookrepo = bookrepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllbooks()
        {
            try
            {
                return Ok(await bookrepo.Getbooks());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBybooks(int id)
        {
            try
            {
                var result = await bookrepo.Getbook(id);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Book>> Addbooks(Book info)
        {
            try
            {
                if (info == null)
                {
                    return BadRequest();
                }
                var result = await bookrepo.Addbook(info);
                return CreatedAtAction(nameof(GetBybooks), new { id = info.Id }, info); // returnning the record
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Book>> Updatebooks(Book info, int id)
        {
            try
            {
                if (id != info.Id)
                {
                    return BadRequest("Book ID is mismatch");
                }
                //var data = await bookrepo.Getbook(id);
                //if (data == null)
                //{
                //    return BadRequest($"Book ID = {id} Not Found");
                //}
                return await bookrepo.Updatebook(info);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Book>> DeleteBook(int id)

        {
            try
            {
                var data = await bookrepo.Getbook(id);
                if (data == null)
                {
                    return BadRequest($"Book ID = {id} Not Found");
                }
                var item = await bookrepo.Deletebook(id);
                if (item)
                {
                    return Ok();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("filter")]
        public async Task<ActionResult<Book>> FilterData(string genre, string author)

        {
            try
            {
                var data = await bookrepo.FilterBooks(genre, author);
                if (data == null)
                {
                    return NotFound("Empty");
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}