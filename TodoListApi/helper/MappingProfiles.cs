using AutoMapper;
using TodoListApi.Dto_s;
using TodoListApi.Models;

namespace TodoListApi.helper
{
    public class MappingProfiles : Profile 
    {
        public MappingProfiles() 
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, CreateUserDto>().ReverseMap();
            CreateMap<User, UpdateUserDto>().ReverseMap();
        }
    }
}
