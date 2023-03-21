namespace library_app_rest.Models;

public class File
{
    public Guid Id { get; set; }
    // public string Name { get; set; }
    public IFormFile Image { get; set; }
}