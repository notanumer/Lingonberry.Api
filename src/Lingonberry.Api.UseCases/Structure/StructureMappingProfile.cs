using AutoMapper;
using Lingonberry.Api.Domain.Locations;
using Lingonberry.Api.UseCases.Structure.Common;
using Lingonberry.Api.UseCases.Structure.GetLocations;

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
    }
}
