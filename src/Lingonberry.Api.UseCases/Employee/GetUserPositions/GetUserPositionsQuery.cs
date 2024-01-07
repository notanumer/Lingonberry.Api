using MediatR;

namespace Lingonberry.Api.UseCases.Employee.GetUserPositions;

/// <summary>
/// Query to get users postions.
/// </summary>
public record GetUserPositionsQuery : IRequest<ICollection<string?>>
{
    /// <summary>
    /// Name of location.
    /// </summary>
    public string? LocationName { get; init; }

    /// <summary>
    /// Name of division.
    /// </summary>
    public string? DivisionName { get; init; }

    /// <summary>
    /// Name of department.
    /// </summary>
    public string? DepartmentName { get; init; }

    /// <summary>
    /// Name of group.
    /// </summary>
    public string? GroupName { get; init; }
}
