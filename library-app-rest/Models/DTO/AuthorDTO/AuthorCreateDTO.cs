namespace library_app_rest.Models.DTO;

public class AuthorCreateDTO
{
    public string Name { get; set; }
    public string Bio { get; set; }
    public string? UserId { get; set; }
}