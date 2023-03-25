using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace library_app_rest.Models;

[Table("Author")]
public class Author
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; }
    [Required]
    public string CreatedBy { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Bio { get; set; }
    public List<Book>? Books { get; set; }

    public string? UserId { get; set; }
    [ForeignKey("UserId")]
    public virtual User User { get; set; }
}