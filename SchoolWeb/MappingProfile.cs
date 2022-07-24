using AutoMapper;
using SchoolWeb.Models;

namespace SchoolWeb;

public class MappingProfile : Profile
{
    // Visit: https://code-maze.com/automapper-net-core/
    public MappingProfile()
    {
        CreateMap<RegisterModel, User>()
            .ForMember(user => user.UserName, options => options.MapFrom(x => x.Email));
    }
}