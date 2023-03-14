using AutoMapper;
using AutoMapper.Internal;
using library_app_rest.Models;
using library_app_rest.Models.DTO;

namespace library_app_rest;

public class MappingHelper : Profile
{
    public MappingHelper()
    {
        CreateMap<User, UserDTO>().ReverseMap();
        CreateMap<Book, BookDTO>().ForMember(dto => dto.Categories,
            opt => { opt.MapFrom(b => b.BooksCategories.Select(bc => bc.Category)); });
    }
}