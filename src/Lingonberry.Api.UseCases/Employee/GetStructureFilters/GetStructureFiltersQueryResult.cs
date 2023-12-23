using Lingonberry.Api.UseCases.Employee.GetStructureFilters.Dto;

namespace Lingonberry.Api.UseCases.Employee.GetStructureFilters;

/// <summary>
/// Result of query <see cref="GetStructureFiltersQuery"/>.
/// </summary>
public class GetStructureFiltersQueryResult
{
    /// <summary>
    /// Divisions collction by location.
    /// </summary>
    public ICollection<DivisionByLocationDto> Divisions { get; init; } = new List<DivisionByLocationDto>();
}
