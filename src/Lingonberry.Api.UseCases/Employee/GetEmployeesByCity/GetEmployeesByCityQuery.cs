using System.ComponentModel.DataAnnotations;
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
    required public string LocationName { get; init; }

    /// <summary>
    /// Structure.
    /// </summary>
    public Structure? Structures { get; init; }

    /// <summary>
    /// Employee filter.
    /// </summary>
    public EmployeeFilter? EmployeeFilter { get; init; }
}
