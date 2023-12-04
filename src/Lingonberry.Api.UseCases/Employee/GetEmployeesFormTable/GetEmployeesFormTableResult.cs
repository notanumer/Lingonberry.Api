namespace Lingonberry.Api.UseCases.Employee.GetEmployeesFormTable;

/// <summary>
/// Get employees form table result
/// </summary>
public class GetEmployeesFormTableResult
{
    /// <summary>
    /// Result users.
    /// </summary>
    public List<UserDto> Users { get; set; } = new();

    /// <summary>
    /// Page.
    /// </summary>
    required public int Page { get; set; }
}
