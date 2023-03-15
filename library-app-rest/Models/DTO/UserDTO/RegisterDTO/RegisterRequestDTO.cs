using System.ComponentModel.DataAnnotations;

namespace library_app_rest.Models.DTO.RegisterDTO;

public class RegisterRequestDTO
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string Role { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
}