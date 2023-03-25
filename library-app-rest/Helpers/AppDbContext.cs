using Bogus;
using Bogus.DataSets;
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
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        List<Author> authors = new List<Author>()
        {
            new Author()
            {
                Id = Guid.NewGuid(),
                Name = "Leo Tolstoy",
                Bio =
                    "Count Lev Nikolayevich Tolstoy (; Russian: Лев Николаевич Толстой, tr. Lev Nikoláyevich Tolstóy; [lʲef nʲɪkɐˈlaɪvʲɪtɕ tɐlˈstoj] (listen); 9 September [O.S. 28 August] 1828 – 20 November [O.S. 7 November] 1910), usually referred to in English as Leo Tolstoy, was a Russian writer who is regarded as one of the greatest authors of all time.",
            },
            new Author()
            {
                Id = Guid.NewGuid(),
                Name = "Gustave Flaubert",
                Bio =
                    "Gustave Flaubert (French: [ɡystav flɔbɛʁ]; 12 December 1821 – 8 May 1880) was a French novelist. Highly influential, he has been considered the leading exponent of literary realism in his country. He is known especially for his debut novel Madame Bovary (1857), his Correspondence, and his scrupulous devotion to his art.",
            },
            new Author()
            {
                Id = Guid.NewGuid(),
                Name = "F. Scott Fitzgerald",
                Bio =
                    "Francis Scott Key Fitzgerald (September 24, 1896 – December 21, 1940) was an American fiction writer, whose works helped to illustrate the flamboyance and excess of the Jazz Age.",
            },
            new Author()
            {
                Id = Guid.NewGuid(),
                Name = "Vladimir Nabokov",
                Bio =
                    "Vladimir Vladimirovich Nabokov (; Russian: Влади́мир Влади́мирович Набо́ков, tr. Vladímir Vladímirovich Nabókov; 22 April [O.S. 10 April] 1899 – 2 July 1977) was a Russian-American novelist, poet, translator, and entomologist.",
            },
            new Author()
            {
                Id = Guid.NewGuid(),
                Name = "George Eliot",
                Bio =
                    "Mary Ann Evans (22 November 1819 – 22 December 1880), known by her pen name George Eliot, was an English novelist, poet, journalist, translator and one of the leading writers of the Victorian era.",
            },
            new Author()
            {
                Id = Guid.NewGuid(),
                Name = "Mark Twain",
                Bio =
                    "Samuel Langhorne Clemens (November 30, 1835 – April 21, 1910), better known by his pen name Mark Twain, was an American writer, humorist, entrepreneur, publisher, and lecturer.",
            },
            new Author()
            {
                Id = Guid.NewGuid(),
                Name = "Anton Chekhov",
                Bio =
                    "Anton Pavlovich Chekhov (; Russian: Анто́н Павло́вич Чехо́в, tr. Anton Pavlovich Tchechov, IPA: [ˈantən pɐˈvlovʲɪtɕ tɕɪxˈxof] (listen); 29 January [O.S. 17 January] 1860 – 15 July [O.S. 3 July] 1904) was a Russian playwright and short story writer, who is considered to be among the greatest writers of short fiction in history.",
            },
            new Author()
            {
                Id = Guid.NewGuid(),
                Name = "Marcel Proust",
                Bio =
                    "Marcel Proust (French: [maʁsɛl pʁust]; 10 July 1871 – 18 November 1922) was a French novelist, critic, and essayist best known for his monumental seven-volume novel À la recherche du temps perdu (In Search of Lost Time; published 1913–27).",
            },
            new Author()
            {
                Id = Guid.NewGuid(),
                Name = "William Shakespeare",
                Bio =
                    "William Shakespeare (26 April 1564 (baptised) – 23 April 1616) was an English poet, playwright, and actor, widely regarded as the greatest writer in the English language and the world's pre-eminent dramatist. He is often called England's national poet and the Bard of Avon.",
            }
        };
        foreach (var author in authors)
        {
            author.CreatedBy = "Enea Xharau";
            author.CreatedAt = DateTime.Now;
        }

        List<Category> categories = new List<Category>()
        {
            new Category()
            {
                Id = 1,
                Name = "Classics",
                Priority = 1,
            },
            new Category()
            {
                Id = 2,
                Name = "Fiction",
                Priority = 2,
            },
            new Category()
            {
                Id = 3,
                Name = "Romance",
                Priority = 3,
            },
            new Category()
            {
                Id = 4,
                Name = "Literature",
                Priority = 4,
            },
            new Category()
            {
                Id = 5,
                Name = "Historical Fiction",
                Priority = 3,
            },
            new Category()
            {
                Id = 6,
                Name = "School",
                Priority = 2,
            },
            new Category()
            {
                Id = 7,
                Name = "Novels",
                Priority = 4,
            },
            new Category()
            {
                Id = 8,
                Name = "Victorian",
                Priority = 8,
            },
            new Category()
            {
                Id = 9,
                Name = "Young Adult",
                Priority = 5,
            },
            new Category()
            {
                Id = 10,
                Name = "Short Stories",
                Priority = 1,
            },
            new Category()
            {
                Id = 11,
                Name = "Unfinished",
                Priority = 1,
            },
            new Category()
            {
                Id = 12,
                Name = "Philosophy",
                Priority = 3,
            },
            new Category()
            {
                Id = 13,
                Name = "Plays",
                Priority = 2,
            },
            new Category()
            {
                Id = 14,
                Name = "Poetry",
                Priority = 1,
            },
            new Category()
            {
                Id = 15,
                Name = "Drama",
                Priority = 2,
            }
        };
        foreach (var category in categories)
        {
            category.CreatedBy = "Enea Xharau";
            category.CreatedAt = DateTime.Now;
        }

        List<Book> books = new()
        {
            new Book()
            {
                Id = Guid.NewGuid(),
                Name = "Anna Karenina",
                Description =
                    "Anna Karenina tells of the doomed love affair between the sensuous and rebellious Anna and the dashing officer, Count Vronsky. Tragedy unfolds as Anna rejects her passionless marriage and must endure the hypocrisies of society. Set against a vast and richly textured canvas of nineteenth-century Russia, the novel's seven major characters create a dynamic imbalance, playing out the contrasts of city and country life and all the variations on love and family happiness. While previous versions have softened the robust, and sometimes shocking, quality of Tolstoy's writing, Pevear and Volokhonsky have produced a translation true to his powerful voice. This award-winning team's authoritative edition also includes an illuminating introduction and explanatory notes. Beautiful, vigorous, and eminently readable, this Anna Karenina will be the definitive text for generations to come.",
                AuthorId = authors.First(a => a.Name == "Leo Tolstoy").Id,
            },
            new Book()
            {
                Id = Guid.NewGuid(),
                Name = "Madame Bovary",
                Description =
                    "For daring to peer into the heart of an adulteress and enumerate its contents with profound dispassion, the author of Madame Bovary was tried for 'offenses against morality and religion.' What shocks us today about Flaubert's devastatingly realized tale of a young woman destroyed by the reckless pursuit of her romantic dreams is its pure artistry: the poise of its narrative structure, the opulence of its prose (marvelously captured in the English translation of Francis Steegmuller), and its creation of a world whose minor figures are as vital as its doomed heroine. In reading Madame Bovary, one experiences a work that remains genuinely revolutionary almost a century and a half after its creation.",
                AuthorId = authors.First(a => a.Name == "Gustave Flaubert").Id,
            },
            new Book()
            {
                Id = Guid.NewGuid(),
                Name = "War and Peace",
                Description =
                    "Epic in scale, War and Peace delineates in graphic detail events leading up to Napoleon's invasion of Russia, and the impact of the Napoleonic era on Tsarist society, as seen through the eyes of five Russian aristocratic families.",
                AuthorId = authors.First(a => a.Name == "Leo Tolstoy").Id,
            },
            new Book()
            {
                Id = Guid.NewGuid(),
                Name = "The Great Gatsby",
                Description =
                    "The novel chronicles an era that Fitzgerald himself dubbed the 'Jazz Age'. Following the shock and chaos of World War I, American society enjoyed unprecedented levels of prosperity during the 'roaring' 1920s as the economy soared. At the same time, Prohibition, the ban on the sale and manufacture of alcohol as mandated by the Eighteenth Amendment, made millionaires out of bootleggers and led to an increase in organized crime, for example the Jewish mafia. Although Fitzgerald, like Nick Carraway in his novel, idolized the riches and glamor of the age, he was uncomfortable with the unrestrained materialism and the lack of morality that went with it, a kind of decadence.",
                AuthorId = authors.First(a => a.Name == "F. Scott Fitzgerald").Id,
            },
            new Book()
            {
                Id = Guid.NewGuid(),
                Name = "Lolita",
                Description =
                    "The book is internationally famous for its innovative style and infamous for its controversial subject: the protagonist and unreliable narrator, middle aged Humbert Humbert, becomes obsessed and sexually involved with a twelve-year-old girl named Dolores Haze.",
                AuthorId = authors.First(a => a.Name == "Vladimir Nabokov").Id,
            },
            new Book()
            {
                Id = Guid.NewGuid(),
                Name = "Middlemarch",
                Description =
                    "Middlemarch: A Study of Provincial Life is a novel by George Eliot, the pen name of Mary Anne Evans, later Marian Evans. It is her seventh novel, begun in 1869 and then put aside during the final illness of Thornton Lewes, the son of her companion George Henry Lewes. During the following year Eliot resumed work, fusing together several stories into a coherent whole, and during 1871–72 the novel appeared in serial form. The first one-volume edition was published in 1874, and attracted large sales. Subtitled 'A Study of Provincial Life', the novel is set in the fictitious Midlands town of Middlemarch during the period 1830–32. It has a multiple plot with a large cast of characters, and in addition to its distinct though interlocking narratives it pursues a number of underlying themes, including the status of women, the nature of marriage, idealism and self-interest, religion and hypocrisy, political reform, and education. The pace is leisurely, the tone is mildly didactic (with an authorial voice that occasionally bursts through the narrative), and the canvas is very broad.",
                AuthorId = authors.First(a => a.Name == "George Eliot").Id
            },
            new Book()
            {
                Id = Guid.NewGuid(),
                Name = "The Adventures of Huckleberry Finn",
                Description =
                    "Revered by all of the town's children and dreaded by all of its mothers, Huckleberry Finn is indisputably the most appealing child-hero in American literature. Unlike the tall-tale, idyllic world of Tom Sawyer, The Adventures of Huckleberry Finn is firmly grounded in early reality. From the abusive drunkard who serves as Huckleberry's father, to Huck's first tentative grappling with issues of personal liberty and the unknown, Huckleberry Finn endeavors to delve quite a bit deeper into the complexities — both joyful and tragic of life.",
                AuthorId = authors.First(a => a.Name == "Mark Twain").Id
            },
            new Book()
            {
                Id = Guid.NewGuid(),
                Name = "The Stories of Anton Chekhov",
                Description =
                    "Anton Pavlovich Chekhov was a Russian short-story writer, playwright and physician, considered to be one of the greatest short-story writers in the history of world literature. His career as a dramatist produced four classics and his best short stories are held in high esteem by writers and critics. Chekhov practised as a doctor throughout most of his literary career: 'Medicine is my lawful wife,' he once said, 'and literature is my mistress.'",
                AuthorId = authors.First(a => a.Name == "Anton Chekhov").Id
            },
            new Book()
            {
                Id = Guid.NewGuid(),
                Name = "In Search of Lost Time",
                Description =
                    "Swann's Way, the first part of A la recherche de temps perdu, Marcel Proust's seven-part cycle, was published in 1913. In it, Proust introduces the themes that run through the entire work. The narrator recalls his childhood, aided by the famous madeleine; and describes M. Swann's passion for Odette. The work is incomparable. Edmund Wilson said '[Proust] has supplied for the first time in literature an equivalent in the full scale for the new theory of modern physics.'",
                AuthorId = authors.First(a => a.Name == "Marcel Proust").Id
            },
            new Book()
            {
                Id = Guid.NewGuid(),
                Name = "Hamlet",
                Description =
                    "The Tragedy of Hamlet, Prince of Denmark, or more simply Hamlet, is a tragedy by William Shakespeare, believed to have been written between 1599 and 1601. The play, set in Denmark, recounts how Prince Hamlet exacts revenge on his uncle Claudius, who has murdered Hamlet's father, the King, and then taken the throne and married Gertrude, Hamlet's mother. The play vividly charts the course of real and feigned madness—from overwhelming grief to seething rage—and explores themes of treachery, revenge, incest, and moral corruption.",
                AuthorId = authors.First(a => a.Name == "William Shakespeare").Id,
            },
        };
        foreach (var book in books)
        {
            book.CreatedAt = DateTime.Now;
            book.CreatedBy = "Enea Xharau";
        }

        List<object> tenConnections = new List<object>()
        {
            new
            {
                BooksId = books[0].Id,
                CategoriesId = 1,
            },
            new
            {
                BooksId = books[0].Id,
                CategoriesId = 2,
            },
            new
            {
                BooksId = books[0].Id,
                CategoriesId = 3,
            },
            new
            {
                BooksId = books[1].Id,
                CategoriesId = 1,
            },
            new
            {
                BooksId = books[1].Id,
                CategoriesId = 2,
            },
            new
            {
                BooksId = books[1].Id,
                CategoriesId = 3,
            },
            new
            {
                BooksId = books[1].Id,
                CategoriesId = 4,
            },
            new
            {
                BooksId = books[2].Id,
                CategoriesId = 1,
            },
            new
            {
                BooksId = books[2].Id,
                CategoriesId = 2,
            },
            new
            {
                BooksId = books[2].Id,
                CategoriesId = 5,
            },
            new
            {
                BooksId = books[2].Id,
                CategoriesId = 4,
            },
            new
            {
                BooksId = books[3].Id,
                CategoriesId = 1,
            },
            new
            {
                BooksId = books[3].Id,
                CategoriesId = 2,
            },
            new
            {
                BooksId = books[3].Id,
                CategoriesId = 6,
            },
            new
            {
                BooksId = books[3].Id,
                CategoriesId = 5,
            },
            new
            {
                BooksId = books[4].Id,
                CategoriesId = 1,
            },
            new
            {
                BooksId = books[4].Id,
                CategoriesId = 4,
            },
            new
            {
                BooksId = books[4].Id,
                CategoriesId = 7,
            },
            new
            {
                BooksId = books[5].Id,
                CategoriesId = 1,
            },
            new
            {
                BooksId = books[5].Id,
                CategoriesId = 2,
            },
            new
            {
                BooksId = books[5].Id,
                CategoriesId = 5,
            },
            new
            {
                BooksId = books[5].Id,
                CategoriesId = 8,
            },
            new
            {
                BooksId = books[6].Id,
                CategoriesId = 1,
            },
            new
            {
                BooksId = books[6].Id,
                CategoriesId = 2,
            },
            new
            {
                BooksId = books[6].Id,
                CategoriesId = 9,
            },
            new
            {
                BooksId = books[7].Id,
                CategoriesId = 10,
            },
            new
            {
                BooksId = books[7].Id,
                CategoriesId = 2,
            },
            new
            {
                BooksId = books[7].Id,
                CategoriesId = 1,
            },
            new
            {
                BooksId = books[7].Id,
                CategoriesId = 11,
            },
            new
            {
                BooksId = books[8].Id,
                CategoriesId = 1,
            },
            new
            {
                BooksId = books[8].Id,
                CategoriesId = 2,
            },
            new
            {
                BooksId = books[8].Id,
                CategoriesId = 4,
            },
            new
            {
                BooksId = books[8].Id,
                CategoriesId = 12,
            },
            new
            {
                BooksId = books[9].Id,
                CategoriesId = 13,
            },
            new
            {
                BooksId = books[9].Id,
                CategoriesId = 1,
            },
            new
            {
                BooksId = books[9].Id,
                CategoriesId = 14,
            },
            new
            {
                BooksId = books[9].Id,
                CategoriesId = 15,
            },
        };
        modelBuilder.Entity<Author>().HasOne(a => a.User).WithOne(u => u.Author).IsRequired(false);
        modelBuilder.Entity<Author>().HasData(authors);
        modelBuilder.Entity<Book>().HasData(books);
        modelBuilder.Entity<Category>().HasData(categories);
        modelBuilder.Entity("BookCategory").HasData(tenConnections.ToList());
        base.OnModelCreating(modelBuilder);
    }
}