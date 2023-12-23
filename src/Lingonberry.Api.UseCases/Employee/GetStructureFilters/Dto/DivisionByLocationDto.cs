namespace Lingonberry.Api.UseCases.Employee.GetStructureFilters.Dto;

/// <summary>
/// Divisions for filter.
/// </summary>
public record DivisionByLocationDto
{
    /// <summary>
    /// Name of division.
    /// </summary>
    required public string Name { get; init; }
}
