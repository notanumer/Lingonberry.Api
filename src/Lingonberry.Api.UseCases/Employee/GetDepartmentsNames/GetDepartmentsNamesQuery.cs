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
    /// Names of divisions.
    /// </summary>
    public ICollection<string?> DivisonNames { get; init; } = new List<string?>();
}
