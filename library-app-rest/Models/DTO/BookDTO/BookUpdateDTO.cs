using System.ComponentModel.DataAnnotations;

namespace library_app_rest.Models.DTO.BookDTO;

public class BookUpdateDTO
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    // public string Image { get; set; }
    [Required]
    public List<Category> Categories { get; set; } = new List<Category>();
}