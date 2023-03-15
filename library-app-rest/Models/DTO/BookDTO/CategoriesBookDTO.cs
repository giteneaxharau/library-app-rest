using System.ComponentModel.DataAnnotations;

namespace library_app_rest.Models.DTO.BookDTO;

public class CategoriesBookDTO
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public int Priority { get; set; }
}