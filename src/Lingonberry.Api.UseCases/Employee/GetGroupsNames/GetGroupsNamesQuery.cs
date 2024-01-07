using MediatR;

namespace Lingonberry.Api.UseCases.Employee.GetGroupsNames;

/// <summary>
/// Query to get groups names by department, division, location.
/// </summary>
public class GetGroupsNamesQuery : IRequest<ICollection<string?>>
{
    /// <summary>
    /// Name of location.
    /// </summary>
    public string? LocationName { get; init; }

    /// <summary>
    /// Name of division in selected location.
    /// </summary>
    public string? DivisionName { get; init; }

    /// <summary>
    /// Name of department in division or location.
    /// </summary>
    public string? DepartmentName { get; init; }
}
