using Lingonberry.Api.UseCases.Users.GetUserById;

namespace Lingonberry.Api.UseCases.Employee.GetEmployeesFormTable;

/// <summary>
/// Get employees form table result
/// </summary>
public class GetEmployeesFormTableResult
{
    /// <summary>
    /// Result users.
    /// </summary>
    public ICollection<UserDetailsDto> Items { get; set; } = new List<UserDetailsDto>();

    /// <summary>
    /// Page.
    /// </summary>
    required public int Page { get; set; }

    /// <summary>
    /// Total count of items.
    /// </summary>
    public int Total { get; set; }
}
