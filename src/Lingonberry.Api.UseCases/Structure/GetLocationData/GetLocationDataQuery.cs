using Lingonberry.Api.UseCases.Structure.GetLocationData.Dto;
using MediatR;

namespace Lingonberry.Api.UseCases.Structure.GetLocationData;

/// <summary>
/// Get division by location name.
/// </summary>
public record GetLocationDataQuery : IRequest<List<GetLocationDataDto>>
{
    /// <summary>
    /// Location name.
    /// </summary>
    required public string Location { get; init; }
}
