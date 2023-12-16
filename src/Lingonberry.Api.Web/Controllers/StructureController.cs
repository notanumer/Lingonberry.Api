using Lingonberry.Api.UseCases.Structure.Common;
using Lingonberry.Api.UseCases.Structure.GetLocationById;
using Lingonberry.Api.UseCases.Structure.GetLocations;
using Lingonberry.Api.UseCases.Users.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lingonberry.Api.Web.Controllers;

/// <summary>
/// Excel controller.
/// </summary>
[ApiController]
[Route("api/structure")]
[ApiExplorerSettings(GroupName = "structure")]
public class StructureController
{
    private readonly IMediator mediator;

    /// <summary>
    /// Constructor.
    /// </summary>
    public StructureController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    /// <summary>
    /// Rest for get locations.
    /// </summary>
    /// <param name="query">GetLocationsQuery.</param>
    /// <returns>List locations.</returns>
    [HttpGet("getLocations")]
    public async Task<List<LocationDto>> GetLocations([FromQuery] GetLocationsQuery query)
    {
        return await mediator.Send(query);
    }

    /// <summary>
    /// Get user by id.
    /// </summary>
    /// <param name="query">GetLocationByIdQuery.</param>
    /// <returns>LocationDto.</returns>
    [HttpGet("getLocationById")]
    public async Task<LocationDto> GetLocationById([FromQuery] GetLocationByIdQuery query)
    {
        return await mediator.Send(query);
    }

    /// <summary>
    /// Get user by id.
    /// </summary>
    /// <param name="query">GetUserByIdQuery.</param>
    /// <returns>UserDetailsDto.</returns>
    [HttpGet("getUserById")]
    public async Task<UserDetailsDto> GetUserById([FromQuery] GetUserByIdQuery query)
    {
        return await mediator.Send(query);
    }
}
