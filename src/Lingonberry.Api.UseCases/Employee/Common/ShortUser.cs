using Lingonberry.Api.Domain.Users;

namespace Lingonberry.Api.UseCases.Employee.Common;

/// <summary>
/// Short user.
/// </summary>
public record ShortUser
{
    /// <summary>
    /// Id.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Full name.
    /// </summary>
    public string? FullName { get; set; }

    /// <summary>
    /// User position.
    /// </summary>
    public string? Position { get; set; }

    /// <summary>
    /// Position value.
    /// </summary>
    public PositionValue UserPosition { get; set; }

    /// <summary>
    /// Is vacancy.
    /// </summary>
    public bool IsVacancy { get; set; }
}
