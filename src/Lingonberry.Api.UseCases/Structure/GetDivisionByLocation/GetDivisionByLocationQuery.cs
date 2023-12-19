using MediatR;

namespace Lingonberry.Api.UseCases.Structure.GetDivisionByLocation;

/// <summary>
/// Get division by location name.
/// </summary>
public class GetDivisionByLocationQuery : IRequest<List<GetDivisionByLocationQueryResult>>
{
    /// <summary>
    /// Location name.
    /// </summary>
    required public string Location { get; init; }
}
