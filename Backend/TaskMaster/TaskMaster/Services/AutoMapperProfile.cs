using AutoMapper;
using TaskMaster.Entities.DTOs;
using TaskMaster.Entities.Models;
using Task = TaskMaster.Entities.Models.Task;

namespace TaskMaster.Services;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<Task, TaskDto>();
    }
}