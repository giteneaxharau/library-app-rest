using System.ComponentModel.DataAnnotations;
using library_app_rest.Models.DTO.BookDTO;

namespace library_app_rest.Models.DTO.CategoryDTO;

public class BookCategoryDTO
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    public AuthorBookDTO Author { get; set; }
    public List<string>? Images { get; set; }
}