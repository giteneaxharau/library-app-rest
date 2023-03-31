using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using library_app_rest.Helpers;
using library_app_rest.Models;
using library_app_rest.Models.DTO;
using library_app_rest.Models.DTO.LoginDTO;
using library_app_rest.Models.DTO.RegisterDTO;
using library_app_rest.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace library_app_rest.Repository;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IMapper _mapper;
    private string secretKey;

    public UserRepository(
        AppDbContext db,
        IConfiguration configuration,
        UserManager<User> userManager, IMapper mapper,
        RoleManager<IdentityRole> roleManager
    )
    {
        _db = db;
        _userManager = userManager;
        _roleManager = roleManager;
        _mapper = mapper;
        this.secretKey = configuration.GetValue<string>("ApiSettings:Secret");
    }

    public async Task<List<UserDTO>> GetAll(bool onlyAuthors = false)
    {
        var users = await _userManager.GetUsersInRoleAsync(onlyAuthors ? "Author" : "Admin");
        return _mapper.Map<List<UserDTO>>(users);
    }

    public bool IsUniqueUser(string username)
    {
        User? user = _db.Users.FirstOrDefault(x => x.UserName == username);
        if (user == null) return true;
        return false;
    }

    public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDto)
    {
        User? user =
            await _db.Users.FirstOrDefaultAsync(u => u.UserName.ToLower() == loginRequestDto.UserName.ToLower());
        bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
        if (user == null || isValid == false)
        {
            return new LoginResponseDTO()
            {
                Token = "",
                User = null
            };
        }

        var roles = await _userManager.GetRolesAsync(user);
        // Generate JWT Token
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(secretKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.UserName.ToString()),
                new Claim(ClaimTypes.Role, roles.FirstOrDefault())
            }),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        LoginResponseDTO loginResponseDto = new()
        {
            User = _mapper.Map<UserDTO>(user),
            Token = tokenHandler.WriteToken(token),
            Role = roles.FirstOrDefault()
        };
        return loginResponseDto;
    }

    public async Task<UserDTO> Register(RegisterRequestDTO registrationRequestDto)
    {
        User user = new()
        {
            UserName = registrationRequestDto.UserName,
            FirstName = registrationRequestDto.FirstName,
            LastName = registrationRequestDto.LastName,
            Email = registrationRequestDto.Email,
            NormalizedEmail = registrationRequestDto.Email.ToUpper(),
        };
        try
        {
            var result = await _userManager.CreateAsync(user, registrationRequestDto.Password);
            if (result.Succeeded)
            {
                if (!_roleManager.RoleExistsAsync("Admin").GetAwaiter().GetResult())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("Author"));
                }

                await _userManager.AddToRoleAsync(user, registrationRequestDto.Role);
                var userToReturn = await _db.Users.FirstOrDefaultAsync(u=>u.UserName == registrationRequestDto.UserName);
                return _mapper.Map<UserDTO>(userToReturn);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return null;
    }
}