using System.ComponentModel;
using Lingonberry.Api.Domain.Locations.Helpers;
using MediatR;

namespace Lingonberry.Api.UseCases.Employee.GetEmployeesByCity;

/// <summary>
/// Get employees by city query.
/// </summary>
public class GetEmployeesByCityQuery : IRequest<GetEmployeesByCityResult>
{
    /// <summary>
    /// Location name.
    /// </summary>
    [DefaultValue("Брусника.Екатеринбург")]
    required public string LocationName { get; init; }
}
