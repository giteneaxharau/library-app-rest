using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace library_app_rest.Models;

[Table("Books")]
public class Book
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
    
    
    public Guid AuthorId { get; set; }
    [ForeignKey("AuthorId")]
    public Author Author { get; set; }
    public virtual ICollection<Category> Categories { get; set; }

}