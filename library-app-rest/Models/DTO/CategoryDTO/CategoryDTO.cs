using System.ComponentModel.DataAnnotations;

namespace library_app_rest.Models.DTO.CategoryDTO;

public class CategoryDTO
{
    [Required]
    public int Id { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string CreatedBy { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public int Priority { get; set; }
    public List<BookCategoryDTO>? Books { get; set; }
}