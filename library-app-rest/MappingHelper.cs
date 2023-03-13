using AutoMapper;
using library_app_rest.Models;
using library_app_rest.Models.DTO;

namespace library_app_rest;

public class MappingHelper: Profile
{
    public MappingHelper()
    {
        CreateMap<User, UserDTO>().ReverseMap();
    }
}