using AutoMapper;
using Model.Dtos;
using Model.Entities;
using Task = Model.Entities.Task;

namespace Api.Services;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<Task, TaskDto>();
    }
}