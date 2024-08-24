using AutoMapper;
using Model.Dtos;
using Model.Entities;

namespace Api.Services;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<Assignment, AssigmentDto>()
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User!.UserName));
        ;
    }
}