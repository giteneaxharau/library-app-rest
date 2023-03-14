using System.Net;
using System.Text.Json;
using AutoMapper;
using library_app_rest.Models;
using library_app_rest.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace library_app_rest.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController: ControllerBase
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
    public async Task<ActionResult<Response>> GetBooks([FromQuery] string? search, int pageSize = 0, int pageNumber = 1)
    {
        try
        {
            IEnumerable<Book> bookList;
            bookList = await _dbBook.GetAll(pageSize: pageSize, pageNumber: pageNumber);
            if (!string.IsNullOrEmpty(search))
            {
                bookList = bookList.Where(u=>u.Name.ToLower().Contains(search.ToLower()));
            }
            List<BookDTO> booktest = new List<BookDTO>();
            foreach (var book in bookList)
            {
                booktest.Add(_mapper.Map<BookDTO>(book));
            }
            Pagination pagination = new Pagination(){ PageNumber = pageNumber, PageSize = pageSize};
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));
            _response.Result = bookList;
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