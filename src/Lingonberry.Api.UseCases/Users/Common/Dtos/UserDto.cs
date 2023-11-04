namespace Lingonberry.Api.UseCases.Users.Common.Dtos;

/// <summary>
/// User DTO.
/// </summary>
public class UserDto
{
    /// <summary>
    /// User identifier.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Full user name.
    /// </summary>
    required public string FullName { get; set; }
}
