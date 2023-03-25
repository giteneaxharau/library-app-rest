using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text.Json;
using AutoMapper;
using library_app_rest.Models;
using library_app_rest.Models.DTO;
using library_app_rest.Repository.IRepository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace library_app_rest.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class AuthorController : ControllerBase
{
    protected Response _response;
    private readonly IAuthorRepository _dbAuthor;
    private readonly IMapper _mapper;

    public AuthorController(IAuthorRepository dbAuthor, IMapper mapper, IFileService fileService)
    {
        _dbAuthor = dbAuthor;
        _mapper = mapper;
        this._response = new();
    }

    [HttpGet]
    [ResponseCache(CacheProfileName = "Default30")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response>> GetAuthors([FromQuery] string? search, [FromQuery] bool? include = false,
        int pageSize = 0, int pageNumber = 1)
    {
        try
        {
            IEnumerable<Author> authorList;
            authorList = await _dbAuthor.GetAll(pageSize: pageSize, pageNumber: pageNumber, include: include);
            if (!string.IsNullOrEmpty(search))
            {
                authorList = authorList.Where(u => u.Name.ToLower().Contains(search.ToLower()));
            }

            List<AuthorDTO> authorListDTO = _mapper.Map<List<AuthorDTO>>(authorList.ToList());
            Pagination pagination = new Pagination() { PageNumber = pageNumber, PageSize = pageSize };
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));
            _response.Result = authorListDTO;
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

    [HttpGet("{id:guid}", Name = "GetAuthor")]
    [ResponseCache(CacheProfileName = "Default30")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response>> GetAuthor(Guid id)
    {
        try
        {
            var author = await _dbAuthor.Get(u => u.Id == id);
            if (author == null)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.ErrorMessages.Add($"Author with id {id} not found");
                return NotFound(_response);
            }

            _response.Result = _mapper.Map<AuthorDTO>(author);
            return Ok(_response);
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages.Add(e.ToString());
        }

        return _response;
    }

    [HttpPost(Name = "CreateAuthor")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response>> CreateAuthor([FromBody] AuthorCreateDTO author)
    {
        try
        {
            var stream = await HttpContext.GetTokenAsync("Bearer", "access_token");
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(stream);
            var userName = jwtSecurityToken.Claims.FirstOrDefault(claim=>claim.Type == "unique_name").Value;
            if (await _dbAuthor.Get(u => u.Name.ToLower() == author.Name.ToLower()) != null)
            {
                ModelState.AddModelError("ExistingError", "Author already exists!");
                return BadRequest(ModelState);
            }

            if (author == null)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("Author object is null");
                return BadRequest(_response);
            }

            if (!ModelState.IsValid)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("Invalid model object");
                return BadRequest(_response);
            }

            Author authorEntity = _mapper.Map<Author>(author);
            authorEntity.Id = Guid.NewGuid();
            authorEntity.CreatedAt = DateTime.Now;
            authorEntity.CreatedBy = userName;
            await _dbAuthor.Create(authorEntity);
            _response.Result = _mapper.Map<AuthorDTO>(authorEntity);
            return CreatedAtRoute("CreateAuthor", new { id = "heheheheh"}, _response);
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.StatusCode = HttpStatusCode.InternalServerError;
            _response.ErrorMessages.Add(e.ToString());
        }

        return _response;
    }

    [HttpDelete("{id:guid}", Name = "DeleteAuthor")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Response>> DeleteAuthor(Guid id)
    {
        try
        {
            if (id == null || id == Guid.Empty)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("Author id is empty");
                return BadRequest(_response);
            }

            var author = await _dbAuthor.Get(u => u.Id == id);
            if (author == null)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.ErrorMessages.Add($"Author with id {id} not found");
                return NotFound(_response);
            }

            await _dbAuthor.Remove(author);
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

    [HttpPut("{id:guid}", Name = "UpdateAuthor")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Response>> UpdateAuthor(Guid id, [FromBody] AuthorUpdateDTO authorUpdate)
    {
        try
        {
            if (authorUpdate.Id != id || authorUpdate.Id == null || authorUpdate.Id == Guid.Empty)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("Author id is not the same as the one in the body");
                return BadRequest(_response);
            }

            if (!ModelState.IsValid)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("Invalid model object");
                return BadRequest(_response);
            }

            var authorEntity = _mapper.Map<Author>(authorUpdate);
            await _dbAuthor.Update(authorEntity);
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