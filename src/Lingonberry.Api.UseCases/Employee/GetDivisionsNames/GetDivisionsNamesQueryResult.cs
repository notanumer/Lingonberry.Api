using Lingonberry.Api.UseCases.Employee.GetDivisionsNames.Dto;

namespace Lingonberry.Api.UseCases.Employee.GetDivisionsNames;

/// <summary>
/// Result of query <see cref="GetDivisionsNamesQuery"/>.
/// </summary>
public class GetDivisionsNamesQueryResult
{
    /// <summary>
    /// Divisions collction by location.
    /// </summary>
    public ICollection<GetDivisionsNamesDto> Divisions { get; init; } = new List<GetDivisionsNamesDto>();
}
