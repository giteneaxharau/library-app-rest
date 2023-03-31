using library_app_rest.Models.DTO;
using library_app_rest.Models.DTO.LoginDTO;
using library_app_rest.Models.DTO.RegisterDTO;

namespace library_app_rest.Repository.IRepository;

public interface IUserRepository
{
    bool IsUniqueUser(string username);
    Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDto);
    Task<UserDTO> Register(RegisterRequestDTO registrationRequestDto);

    Task<List<UserDTO>> GetAll(bool onlyAuthors = false);
}