namespace Lingonberry.Api.UseCases.Employee.GetDivisionsNames.Dto;

/// <summary>
/// Divisions for filter.
/// </summary>
public record GetDivisionsNamesDto
{
    /// <summary>
    /// Name of division.
    /// </summary>
    required public string Name { get; init; }
}
