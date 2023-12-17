namespace Lingonberry.Api.UseCases.Users.GetUserById;

public class UserGetByIdDto
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
    required public string Email { get; set; }

    /// <summary>
    /// Phone number.
    /// </summary>
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// User position in company.
    /// </summary>
    public string? Position { get; set; }

    /// <summary>
    /// Is user vacancy.
    /// </summary>
    public bool IsVacancy { get; set; }

    /// <summary>
    /// Work type of user.
    /// </summary>
    public string? WorkType { get; set; }

    /// <summary>
    /// User number.
    /// </summary>
    required public string UserNumber { get; set; }
}
