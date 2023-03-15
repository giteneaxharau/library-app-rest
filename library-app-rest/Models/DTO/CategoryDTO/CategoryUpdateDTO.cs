using System.ComponentModel.DataAnnotations;

namespace library_app_rest.Models.DTO.CategoryDTO;

public class CategoryUpdateDTO
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public int Priority { get; set; }
    public List<BookCategoryDTO>? Books { get; set; }
}