using library_app_rest.Models.DTO.BookDTO;
using library_app_rest.Models.DTO.CategoryDTO;

namespace library_app_rest.Models.DTO;

public class AuthorDTO
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    public string Name { get; set; }
    public string Bio { get; set; }
    public List<BookCategoryDTO>? Books { get; set; }
    public virtual UserDTO? User { get; set; }
}