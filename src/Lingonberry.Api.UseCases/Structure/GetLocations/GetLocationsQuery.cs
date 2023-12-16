using Lingonberry.Api.UseCases.Structure.Common;
using MediatR;

namespace Lingonberry.Api.UseCases.Structure.GetLocations;

/// <summary>
/// Get locations query.
/// </summary>
public class GetLocationsQuery : IRequest<List<LocationDto>>
{
}
