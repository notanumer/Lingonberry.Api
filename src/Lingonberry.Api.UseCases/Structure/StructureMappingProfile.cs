using AutoMapper;
using Lingonberry.Api.Domain.Locations;
using Lingonberry.Api.Domain.Locations.Helpers;
using Lingonberry.Api.UseCases.Structure.Common;
using Lingonberry.Api.UseCases.Structure.GetLocationData;
using Lingonberry.Api.UseCases.Structure.GetLocationData.Dto;

namespace Lingonberry.Api.UseCases.Structure;

/// <summary>
/// User mapping profile.
/// </summary>
public class StructureMappingProfile : Profile
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public StructureMappingProfile()
    {
        CreateMap<Location, LocationDto>();
        CreateMap<Division, GetLocationDataDto>()
            .ForMember(src => src.StructureEnum, dest => dest.MapFrom(d => StructureEnum.Division));
        CreateMap<Group, GetLocationDataDto>()
            .ForMember(src => src.StructureEnum, dest => dest.MapFrom(d => StructureEnum.Group));
        CreateMap<Department, GetLocationDataDto>()
            .ForMember(src => src.StructureEnum, dest => dest.MapFrom(d => StructureEnum.Department));
    }
}
