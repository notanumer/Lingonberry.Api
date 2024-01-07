using MediatR;

namespace Lingonberry.Api.UseCases.Employee.GetDepartmentsNames;

/// <summary>
/// Query to get names of departments
/// selected by location and division names.
/// </summary>
public record GetDepartmentsNamesQuery : IRequest<ICollection<string?>>
{
    /// <summary>
    /// Location name.
    /// </summary>
    public string? LocationName { get; init; }

    /// <summary>
    /// Name of division in location.
    /// </summary>
    public string? DivisonName { get; init; }
}
