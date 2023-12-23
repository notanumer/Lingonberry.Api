using AutoMapper;
using Lingonberry.Api.Domain.Locations;
using Lingonberry.Api.Domain.Users;
using Lingonberry.Api.UseCases.Employee.Common;
using Lingonberry.Api.UseCases.Employee.GetStructureFilters.Dto;

namespace Lingonberry.Api.UseCases.Employee;

/// <summary>
/// Employee mapping profile.
/// </summary>
public class EmployeeMappingProfile : Profile
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public EmployeeMappingProfile()
    {
        CreateMap<User, ShortUser>();
        CreateMap<Division, DivisionByLocationDto>();
    }
}
