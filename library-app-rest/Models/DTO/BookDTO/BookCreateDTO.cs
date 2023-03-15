using System.ComponentModel.DataAnnotations;

namespace library_app_rest.Models.DTO.BookDTO;

public class BookCreateDTO
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    // public string Image { get; set; }
    [Required]
    public List<CategoriesBookDTO> Categories { get; set; } = new List<CategoriesBookDTO>();

}