using System.Net;
using library_app_rest.Models;
using library_app_rest.Models.DTO.LoginDTO;
using library_app_rest.Models.DTO.RegisterDTO;
using library_app_rest.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace library_app_rest.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/UserAuth")]
[ApiVersion("1.0")]
public class UserController: Controller
{
    private readonly IUserRepository _userRepo;
    protected Response _response;

    public UserController(IUserRepository userRepo)
    {
        _userRepo = userRepo;
        this._response = new();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDTO requestDto)
    {
        var loginResponse = await _userRepo.Login(requestDto);
        if (loginResponse.User == null || string.IsNullOrEmpty(loginResponse.Token))
        {
            _response.StatusCode = HttpStatusCode.BadRequest;
            _response.IsSuccess = false;
            _response.ErrorMessages.Add("Username or password is incorrect.");
            return BadRequest(_response);
        }

        _response.StatusCode = HttpStatusCode.OK;
        _response.Result = loginResponse;
        return Ok(_response);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDTO requestDto)
    {
        bool ifUserNameUnique = _userRepo.IsUniqueUser(requestDto.UserName);
        if(!ifUserNameUnique)
        {
            _response.StatusCode = HttpStatusCode.BadRequest;
            _response.IsSuccess = false;
            _response.ErrorMessages.Add("Username already exists.");
            return BadRequest(_response);
        }

        var user = await _userRepo.Register(requestDto);
        if (user == null)
        {
            _response.StatusCode = HttpStatusCode.BadRequest;
            _response.IsSuccess = false;
            _response.ErrorMessages.Add("Error while registering");
            return BadRequest(_response);
        }
        _response.StatusCode = HttpStatusCode.OK;
        _response.IsSuccess = true;
        _response.Result = user;
        return Ok(_response);
    }
}