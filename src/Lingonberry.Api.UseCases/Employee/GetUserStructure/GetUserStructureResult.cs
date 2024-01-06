using Lingonberry.Api.Domain.Locations.Helpers;
using Lingonberry.Api.UseCases.Employee.Common;

namespace Lingonberry.Api.UseCases.Employee.GetUserStructure;

/// <summary>
/// Result user structure.
/// </summary>
public class GetUserStructureResult
{
    /// <summary>
    /// Id.
    /// </summary>
    public int? Id { get; set; }

    /// <summary>
    /// Structure name.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Structure type.
    /// </summary>
    public StructureEnum? StructureEnum { get; set; }

    /// <summary>
    /// Is display.
    /// </summary>
    public bool IsDisplay { get; set; } = true;

    /// <summary>
    /// Next content.
    /// </summary>
    public GetUserStructureResult? Next { get; set; }

    /// <summary>
    /// Employer.
    /// </summary>
    public ICollection<ShortUser> Employers { get; set; } = new List<ShortUser>();

    /// <summary>
    /// Employees.
    /// </summary>
    public ICollection<ShortUser> Employees { get; set; } = new List<ShortUser>();
}
