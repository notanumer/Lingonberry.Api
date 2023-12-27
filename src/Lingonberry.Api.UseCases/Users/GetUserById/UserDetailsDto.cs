namespace Lingonberry.Api.UseCases.Users.GetUserById;

/// <summary>
/// User details.
/// </summary>
public class UserDetailsDto
{
    /// <summary>
    /// User identifier.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Full user name.
    /// </summary>
    required public string FullName { get; set; }

    /// <summary>
    /// User email.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Phone number.
    /// </summary>
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// User position in company.
    /// </summary>
    required public string Position { get; set; }

    /// <summary>
    /// Is user vacancy.
    /// </summary>
    public bool IsVacancy { get; set; }

    /// <summary>
    /// Work type of user.
    /// </summary>
    required public string WorkType { get; set; }

    /// <summary>
    /// User number.
    /// </summary>
    required public string UserNumber { get; set; }

    /// <summary>
    /// User location.
    /// </summary>
    public string? Location { get; set; }

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
    /// Legal entity.
    /// </summary>
    public string? LegalEntity { get; init; }
}
