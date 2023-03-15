namespace library_app_rest.Models.DTO.LoginDTO;

public class LoginResponseDTO
{
    public UserDTO User { get; set; }
    public string Role { get; set; }
    public string Token { get; set; }
}