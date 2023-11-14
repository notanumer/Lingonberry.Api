using System.ComponentModel.DataAnnotations;
using Lingonberry.Api.Domain.Locations;
using Microsoft.AspNetCore.Identity;
using Saritasa.Tools.Common.Utils;

namespace Lingonberry.Api.Domain.Users;

/// <summary>
/// Custom application user entity.
/// </summary>
public class User : IdentityUser<int>
{
    /// <summary>
    /// First name.
    /// </summary>
    [MaxLength(255)]
    public string? FirstName { get; set; }

    /// <summary>
    /// Last name.
    /// </summary>
    [MaxLength(255)]
    public string? LastName { get; set; }

    /// <summary>
    /// Patronymic.
    /// </summary>
    [MaxLength(255)]
    public string? Patronymic { get; set; }

    /// <summary>
    /// Phone number.
    /// </summary>
    [MaxLength(11)]
    public override string? PhoneNumber { get; set; }

    /// <summary>
    /// User salary.
    /// </summary>
    public int? Salary { get; set; }

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
    public WorkType WorkType { get; set; }

    /// <summary>
    /// Full name, concat of first name and last name.
    /// </summary>
    public string FullName => StringUtils.JoinIgnoreEmpty(separator: " ", FirstName, LastName);

    /// <summary>
    /// The date when user last logged in.
    /// </summary>
    public DateTime? LastLogin { get; set; }

    /// <summary>
    /// Last token reset date. Before the date all generate login tokens are considered
    /// not valid. Must be in UTC format.
    /// </summary>
    public DateTime LastTokenResetAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Indicates when the user was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Indicates when the user was updated.
    /// </summary>
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Indicates when the user was removed.
    /// </summary>
    public DateTime? RemovedAt { get; set; }

    public int? DepartmentId { get; set; }

    public Department? Department { get; set; }

    public int? DivisionId { get; set; }

    public Division? Division { get; set; }

    public int? GroupId { get; set; }

    public Group? Group { get; set; }

    public int? LocationId { get; set; }

    public Location? Location { get; set; }
}
