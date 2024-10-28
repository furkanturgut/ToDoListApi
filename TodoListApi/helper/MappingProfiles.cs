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
            CreateMap<TodoTask, CreateTaskDto>().ReverseMap();
            CreateMap<TodoTask, TaskDto>()
            .ForMember(dest => dest.TaskStatus, opt => opt.MapFrom(src => src.TodoStatus.Title));
            CreateMap<TodoTask, UpdateTaskDto>().ReverseMap();
            CreateMap<TodoTask, UpdateTaskStatusDto>().ReverseMap();
        }
    }
}
