using System.Net;
using System.Text.Json;
using AutoMapper;
using library_app_rest.Models;
using library_app_rest.Models.DTO.BookDTO;
using library_app_rest.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace library_app_rest.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    protected Response _response;
    private readonly IBookRepository _dbBook;
    private readonly IMapper _mapper;

    public BooksController(IBookRepository dbBook, IMapper mapper)
    {
        _dbBook = dbBook;
        _mapper = mapper;
        this._response = new();
    }

    [HttpGet]
    [ResponseCache(CacheProfileName = "Default30")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response>> GetBooks([FromQuery] string? search, [FromQuery] bool? include = true,
        int pageSize = 0, int pageNumber = 1)
    {
        try
        {
            IEnumerable<Book> bookList;
            bookList = await _dbBook.GetAll(pageSize: pageSize, pageNumber: pageNumber, include: include);
            if (!string.IsNullOrEmpty(search))
            {
                bookList = bookList.Where(u => u.Name.ToLower().Contains(search.ToLower()));
            }

            List<BookDTO> bookListDTO = _mapper.Map<List<BookDTO>>(bookList.ToList());
            Pagination pagination = new Pagination() { PageNumber = pageNumber, PageSize = pageSize };
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));
            _response.Result = bookListDTO;
            return Ok(_response);
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.StatusCode = HttpStatusCode.InternalServerError;
            _response.ErrorMessages.Add(e.ToString());
        }

        return _response;
    }

    [HttpGet("{id:guid}", Name = "GetBook")]
    [ResponseCache(CacheProfileName = "Default30")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response>> GetBook(Guid id)
    {
        try
        {
            var book = await _dbBook.Get(u => u.Id == id);
            if (book == null)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.ErrorMessages.Add($"Book with id {id} not found");
                return NotFound(_response);
            }

            _response.Result = _mapper.Map<BookDTO>(book);
            return Ok(_response);
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.StatusCode = HttpStatusCode.InternalServerError;
            _response.ErrorMessages.Add(e.ToString());
        }

        return _response;
    }

    [HttpPost(Name = "CreateBook")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response>> CreateBook([FromBody] BookCreateDTO book)
    {
        try
        {
            if (await _dbBook.Get(u => u.Name.ToLower() == book.Name.ToLower()) != null)
            {
                ModelState.AddModelError("ExistingError", "Book already exists!");
                return BadRequest(ModelState);
            }

            if (book.Categories == null)
            {
                ModelState.AddModelError("CategoriesExisting", "This book must have at least one category!");
                return BadRequest(ModelState);
            }

            if (book == null)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("Book object is null");
                return BadRequest(_response);
            }

            if (!ModelState.IsValid)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("Invalid model object");
                return BadRequest(_response);
            }

            Book bookEntity = _mapper.Map<Book>(book);
            bookEntity.Id = Guid.NewGuid();
            bookEntity.CreatedAt = DateTime.Now;
            bookEntity.CreatedBy = "Enea Xharau";
            await _dbBook.Create(bookEntity);
            _response.Result = _mapper.Map<BookDTO>(bookEntity);
            return CreatedAtRoute("CreateBook", new { id = bookEntity.Id }, _response);
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.StatusCode = HttpStatusCode.InternalServerError;
            _response.ErrorMessages.Add(e.ToString());
        }

        return _response;
    }

    [HttpDelete("{id:guid}", Name = "DeleteBook")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Response>> DeleteBook(Guid id)
    {
        try
        {
            if (id == null || id == Guid.Empty)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("Book id is empty");
                return BadRequest(_response);
            }

            var book = await _dbBook.Get(u => u.Id == id);
            if (book == null)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.ErrorMessages.Add($"Book with id {id} not found");
                return NotFound(_response);
            }

            await _dbBook.Remove(book);
            _response.StatusCode = HttpStatusCode.NoContent;
            return Ok(_response);
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.StatusCode = HttpStatusCode.InternalServerError;
            _response.ErrorMessages.Add(e.ToString());
        }

        return _response;
    }

    [HttpPut("{id:guid}", Name = "UpdateBook")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Response>> UpdateBook(Guid id, [FromBody] BookUpdateDTO bookUpdate)
    {
        try
        {
            if (bookUpdate.Id != id || bookUpdate.Id == null || bookUpdate.Id == Guid.Empty)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("Book id is not the same as the one in the body");
                return BadRequest(_response);
            }

            if (!ModelState.IsValid)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("Invalid model object");
                return BadRequest(_response);
            }

            var bookEntity = _mapper.Map<Book>(bookUpdate);
            await _dbBook.Update(bookEntity);
            _response.StatusCode = HttpStatusCode.NoContent;
            return Ok(_response);
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.StatusCode = HttpStatusCode.InternalServerError;
            _response.ErrorMessages.Add(e.ToString());
        }

        return _response;
    }
}