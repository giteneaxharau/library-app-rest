using System.ComponentModel.DataAnnotations;

namespace library_app_rest.Models.DTO.CategoryDTO;

public class BookCategoryDTO
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
}