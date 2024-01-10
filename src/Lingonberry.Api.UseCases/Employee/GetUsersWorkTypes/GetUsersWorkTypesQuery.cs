using MediatR;

namespace Lingonberry.Api.UseCases.Employee.GetUsersWorkTypes;

/// <summary>
/// Query to get users worktypes.
/// </summary>
public record GetUsersWorkTypesQuery : IRequest<ICollection<string>>
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

    /// <summary>
    /// Name of user position.
    /// </summary>
    public string? PositionName { get; init; }
}
