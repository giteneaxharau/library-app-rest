using System.Net;
using System.Text.Json;
using AutoMapper;
using library_app_rest.Models;
using library_app_rest.Models.DTO.CategoryDTO;
using library_app_rest.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace library_app_rest.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class CategoriesController : ControllerBase
{
    protected Response _response;
    private readonly ICategoryRepository _dbCategory;
    private readonly IMapper _mapper;

    public CategoriesController(ICategoryRepository dbCategory, IMapper mapper)
    {
        _dbCategory = dbCategory;
        _mapper = mapper;
        this._response = new();
    }

    [HttpGet]
    [ResponseCache(CacheProfileName = "Default30")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response>> GetCategories([FromQuery] string? search, int pageSize = 0,
        int pageNumber = 1, bool include = false)
    {
        try
        {
            IEnumerable<Category> categoriesList;
            categoriesList = await _dbCategory.GetAll(pageSize: pageSize, pageNumber: pageNumber, include: include);
            if (!string.IsNullOrEmpty(search))
            {
                categoriesList = categoriesList.Where(x => x.Name.ToLower().Contains(search.ToLower()));
            }

            List<CategoryDTO> categories = _mapper.Map<List<CategoryDTO>>(categoriesList);
            Pagination pagination = new Pagination() { PageNumber = pageNumber, PageSize = pageSize };
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));
            _response.Result = categories;
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

    [HttpGet("{id:int}", Name = "GetCategory")]
    [ResponseCache(CacheProfileName = "Default30")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response>> GetCategory(int id, [FromQuery] bool? include = false)
    {
        try
        {
            var category = await _dbCategory.Get(u => u.Id == id, include: include);
            if (category == null)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.ErrorMessages.Add($"Book with id {id} not found");
                return NotFound(_response);
            }

            _response.Result = _mapper.Map<CategoryDTO>(category);
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
    [HttpPost(Name = "CreateCategory")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response>> CreateCategory([FromBody] CategoryCreateDTO categoryCreate)
    {
        try
        {
            if (await _dbCategory.Get(u => u.Name.ToLower() == categoryCreate.Name.ToLower()) != null)
            {
                ModelState.AddModelError("ExistingError", "Category already exists!");
                return BadRequest(ModelState);
            }
            if (categoryCreate == null)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("Category object is null");
                return BadRequest(_response);
            }

            if (!ModelState.IsValid)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("Invalid model object");
                return BadRequest(_response);
            }

            Category categoryEntity = _mapper.Map<Category>(categoryCreate);
            categoryEntity.CreatedAt = DateTime.Now;
            categoryEntity.CreatedBy = "Enea Xharau";
            await _dbCategory.Create(categoryEntity);
            _response.Result = _mapper.Map<CategoryDTO>(categoryEntity);
            return CreatedAtRoute("CreateBook", new { id = categoryEntity.Id }, _response);
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.StatusCode = HttpStatusCode.InternalServerError;
            _response.ErrorMessages.Add(e.ToString());
        }

        return _response;
    }
    
    [HttpDelete("{id:int}", Name = "DeleteCategory")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Response>> DeleteBook(int id)
    {
        try
        {
            if (id == null || id == 0)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("Category id is empty");
                return BadRequest(_response);
            }

            var category = await _dbCategory.Get(u => u.Id == id);
            if (category == null)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.ErrorMessages.Add($"Category with id {id} not found");
                return NotFound(_response);
            }
            await _dbCategory.Remove(category);
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
    
    [HttpPut("{id:int}", Name = "UpdateCategory")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Response>> UpdateBook(int id, [FromBody] CategoryUpdateDTO categoryUpdate)
    {
        try
        {
            if (categoryUpdate.Id != id || categoryUpdate.Id == null || categoryUpdate.Id == 0)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("Category id is not the same as the one in the body");
                return BadRequest(_response);
            }

            if (!ModelState.IsValid)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("Invalid model object");
                return BadRequest(_response);
            }

            var categoryEntity = _mapper.Map<Category>(categoryUpdate);
            await _dbCategory.Update(categoryEntity);
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