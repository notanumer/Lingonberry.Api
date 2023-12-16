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
    /// Is vacancy.
    /// </summary>
    public bool IsVacancy { get; set; }
}
