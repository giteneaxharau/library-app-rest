using System.Collections.ObjectModel;
using AutoMapper;
using AutoMapper.Internal;
using library_app_rest.Models;
using library_app_rest.Models.DTO;
using library_app_rest.Models.DTO.BookDTO;
using library_app_rest.Models.DTO.CategoryDTO;

namespace library_app_rest;

public class MappingHelper : Profile
{
    public MappingHelper()
    {
        //Book table mappers
        CreateMap<AuthorBookDTO, Author>().ReverseMap();
        CreateMap<CategoriesBookDTO, Category>().ReverseMap();
        CreateMap<Book, BookDTO>().ReverseMap();
        CreateMap<BookCreateDTO, Book>().ReverseMap();
        CreateMap<Book, BookUpdateDTO>().ReverseMap();

        //Category Table mappers
        CreateMap<Book, BookCategoryDTO>().ReverseMap();
        CreateMap<Category, CategoryDTO>().ReverseMap();
        CreateMap<Category, CategoryCreateDTO>().ReverseMap();
        CreateMap<Category, CategoryUpdateDTO>().ReverseMap();

        //User table mappers
        CreateMap<User, UserDTO>().ReverseMap();
        
        //Author table mappers
        CreateMap<Author, AuthorDTO>().ReverseMap();
        CreateMap<Author, AuthorCreateDTO>().ReverseMap();
        CreateMap<Author, AuthorUpdateDTO>().ReverseMap();
    }
}