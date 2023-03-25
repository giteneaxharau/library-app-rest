using System.ComponentModel.DataAnnotations;
using library_app_rest.Models.DTO.BookDTO;

namespace library_app_rest.Models;

public class BookDTO
{
    [Key]
    public Guid Id { get; set; }
    [Required]    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    [Required]
    public string CreatedBy { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    public AuthorBookDTO Author { get; set; }
    public List<string>? Images { get; set; }
    [Required]
    public List<CategoriesBookDTO> Categories { get; set; }

}