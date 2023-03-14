using System.ComponentModel.DataAnnotations.Schema;

namespace library_app_rest.Models;

[Table("BooksCategories")]
public class BookCategory
{
    public Guid BookId { get; set; }
    public Book Book { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    
}