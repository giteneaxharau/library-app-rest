using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
namespace library_app_rest.Models;

public class User: IdentityUser {
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    public string Bio { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}