namespace library_app_rest.Models.DTO;

public class AuthorUpdateDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Bio { get; set; }
    public string? UserId { get; set; }
}