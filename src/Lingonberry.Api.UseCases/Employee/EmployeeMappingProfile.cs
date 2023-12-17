using AutoMapper;
using Lingonberry.Api.Domain.Users;
using Lingonberry.Api.UseCases.Employee.Common;
using Microsoft.AspNetCore.Identity;

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
    }
}
