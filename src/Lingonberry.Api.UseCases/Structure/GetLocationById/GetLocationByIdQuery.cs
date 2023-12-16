using Lingonberry.Api.UseCases.Structure.Common;
using MediatR;

namespace Lingonberry.Api.UseCases.Structure.GetLocationById;

/// <summary>
/// Get location by id.
/// </summary>
public class GetLocationByIdQuery : IRequest<LocationDto>
{
    /// <summary>
    /// Location id.
    /// </summary>
    required public int Id { get; init; }
}
