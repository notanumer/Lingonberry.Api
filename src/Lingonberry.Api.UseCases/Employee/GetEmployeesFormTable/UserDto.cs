using Lingonberry.Api.Domain.Users;

namespace Lingonberry.Api.UseCases.Employee.GetEmployeesFormTable;

/// <summary>
/// User dto.
/// </summary>
public class UserDto
{
    /// <summary>
    /// Full name.
    /// </summary>
    required public string FullName { get; init; }

    /// <summary>
    /// User number.
    /// </summary>
    required public string UserNumber { get; init; }

    /// <summary>
    /// Legal entity.
    /// </summary>
    required public string LegalEntity { get; init; }

    /// <summary>
    /// Location.
    /// </summary>
    public string? Location { get; init; }

    /// <summary>
    /// Division.
    /// </summary>
    public string? Division { get; init; }

    /// <summary>
    /// Department.
    /// </summary>
    public string? Department { get; init; }

    /// <summary>
    /// Group.
    /// </summary>
    public string? Group { get; init; }

    /// <summary>
    /// Position.
    /// </summary>
    required public PositionValue Position { get; init; }

    /// <summary>
    /// Work type.
    /// </summary>
    required public WorkType WorkType { get; init; }
}
