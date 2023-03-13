using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace library_app_rest.Models;

[Table("Categories")]
public class Category
{
    public Category()
    {
        this.Books = new HashSet<Book>();
    }
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string CreatedBy { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public int Priority { get; set; }
    public virtual ICollection<Book> Books { get; set; }
}