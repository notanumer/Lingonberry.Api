using AutoMapper;
using Lingonberry.Api.Domain.Users;
using Lingonberry.Api.UseCases.Handlers;
using Lingonberry.Api.UseCases.Users.Common.Dtos;
using Lingonberry.Api.UseCases.Users.GetUserById;

namespace Lingonberry.Api.UseCases.Users;

/// <summary>
/// User mapping profile.
/// </summary>
public class UserMappingProfile : Profile
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public UserMappingProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<User, UserDetailsDto>()
            .ForMember(dest => dest.WorkType, opt => opt.MapFrom(src => DisplayEnum.GetValueFromEnum(src.WorkType)));
    }
}
