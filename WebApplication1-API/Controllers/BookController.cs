using Microsoft.AspNetCore.Mvc;
using WebApplication1_API.Repository;
using DataAccessLayers;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication1_API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class BookController : ControllerBase 
    {
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        //Getting All Book Record
        [HttpGet]
        public async Task<ActionResult> GetAllBooks()
        {
            try
            {
                return Ok(await _bookRepository.GetAllBooks());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error In Retrving record from Db");
            }
        }
        //Get Book By ID

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<Book>>GetBook(int Id)
        {
            try

            {
                var record = await _bookRepository.GetBook(Id);
                if (record == null)
                {
                    return NotFound();
                }
                return record;

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error In Retrving record from Db");
            }
        }

        //Add New Book
        [HttpPost()]
        public async Task<ActionResult<Book>> CreateEmpolyee(Book book)
        {
            try
            {
                if (book == null)
                {
                    return BadRequest();
                }
                var newBook = await _bookRepository.AddNewBook(book);
                return CreatedAtAction(nameof(GetBook), new { Id = newBook.Id }, newBook);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error In Retrving record from Db");
            }
        }

        //Update New Book
        
        [HttpPatch("{Id:int}")]
        
        public async Task<ActionResult<Book>> UpdateBook(int Id, Book book)
        {
            try
            {
                if (Id != book.Id)
                {
                    return BadRequest("Id Mismatch");
                }
                var bookUpdate = await _bookRepository.UpdateBook(book);
                if (bookUpdate == null)
                {
                    return NotFound($"Book Id={Id} not found");
                }
                return await _bookRepository.UpdateBook(book);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error In Retrving record from Db");
            }
        }

        //Delete Empolyee 
        [HttpDelete("{Id:int}")]
        public async Task<ActionResult<Book>> DeleteEmployee(int Id)
        {
            try
            {
                var bookDelete = await _bookRepository.GetBook(Id);
                if (bookDelete == null)
                {
                    return NotFound($"Empolyee Id={Id} not Found");
                }
                return await _bookRepository.DeleteBook(Id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error In Retrving record from Db");
            }
        }

        [HttpGet("{search}")]
        public async Task<ActionResult<Book>> Search(string BookName)
        {
            try
            {
                var data = await _bookRepository.Search(BookName);
                if (data.Any())
                {
                    return Ok(data);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error In Retrving record from Db");
            }
        }



    }
}
