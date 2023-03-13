using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace library_app_rest.Models;

[Table("Books")]
public class Book
{
    public Book()
    {
        this.Categories = new HashSet<Category>();
    }

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
    public virtual ICollection<Category> Categories { get; set; }

}