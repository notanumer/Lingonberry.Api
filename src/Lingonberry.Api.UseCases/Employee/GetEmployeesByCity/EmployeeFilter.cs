namespace Lingonberry.Api.UseCases.Employee.GetEmployeesByCity;

/// <summary>
/// Employee filter.
/// </summary>
public class EmployeeFilter
{
    /// <summary>
    /// Full name.
    /// </summary>
    required public bool IsFullName { get; init; }

    /// <summary>
    /// User position.
    /// </summary>
    required public bool IsPosition { get; init; }

    /// <summary>
    /// Work type.
    /// </summary>
    required public bool IsWorkType { get; init; }
}
