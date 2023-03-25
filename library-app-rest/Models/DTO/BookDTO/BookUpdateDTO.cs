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
    [Required]
    public Guid AuthorId { get; set; }
    [Required]
    public List<CategoriesBookDTO> Categories { get; set; } = new List<CategoriesBookDTO>();
}