using System.ComponentModel.DataAnnotations;

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
    // public string Image { get; set; }
    public List<Category> Categories { get; set; }

}