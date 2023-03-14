using Bogus;
using library_app_rest.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace library_app_rest.Helpers;

public enum Gender
{
    Male,
    Female
}

public class AppDbContext : IdentityDbContext<User>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<BookCategory> BooksCategories { get; set; }
    public DbSet<Book> Books { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var seedBook = new Faker<Book>()
            .RuleFor(u => u.Id, (f, u) => Guid.NewGuid())
            .RuleFor(u => u.CreatedAt, (f, u) => DateTime.Now)
            .RuleFor(u => u.CreatedBy, (f, u) => f.Name.FullName())
            .RuleFor(u => u.Name, (f, u) => f.Commerce.ProductName())
            .RuleFor(u => u.Description, (f, u) => f.Lorem.Sentence());
        var seedCategories = new Faker<Category>()
            .RuleFor(u => u.Id, (f, u) => f.UniqueIndex)
            .RuleFor(u => u.CreatedBy, (f, u) => f.Name.FullName())
            .RuleFor(u => u.CreatedAt, (f, u) => DateTime.Now)
            .RuleFor(u => u.Name, (f, u) => f.Commerce.Categories(1)[0])
            .RuleFor(u => u.Priority, (f, u) => f.Random.Int(1, 10));

        var tenBooks = seedBook.Generate(10);
        var tenCategories = seedCategories.Generate(10);
        modelBuilder.Entity<BookCategory>().HasKey(bc => new { bc.BookId, bc.CategoryId });
        modelBuilder.Entity<Book>().HasData(tenBooks);
        modelBuilder.Entity<Category>().HasData(tenCategories);
        List<BookCategory> tenConnection = new List<BookCategory>();
        for (int i = 0; i < tenCategories.Count(); i++)
        {
            tenConnection.Add(new BookCategory()
            {
                CategoryId = tenCategories[i].Id,
                BookId = tenBooks[i].Id
            });
        }

        modelBuilder.Entity<BookCategory>()
            .HasOne<Book>(bc => bc.Book)
            .WithMany(b => b.BooksCategories);
        modelBuilder.Entity<BookCategory>()
            .HasOne(bc => bc.Category)
            .WithMany(c => c.BooksCategories);
        modelBuilder.Entity<BookCategory>().HasData(tenConnection.ToList());
        base.OnModelCreating(modelBuilder);
    }
}