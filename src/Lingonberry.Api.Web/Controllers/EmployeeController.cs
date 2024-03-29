﻿using Lingonberry.Api.UseCases.Employee.GetDepartmentsNames;
using Lingonberry.Api.UseCases.Employee.GetDivisionsNames;
using Lingonberry.Api.UseCases.Employee.GetEmployeesByCity;
using Lingonberry.Api.UseCases.Employee.GetEmployeesFormTable;
using Lingonberry.Api.UseCases.Employee.GetGroupsNames;
using Lingonberry.Api.UseCases.Employee.GetLocationsNames;
using Lingonberry.Api.UseCases.Employee.GetUserPositions;
using Lingonberry.Api.UseCases.Employee.GetUserStructure;
using Lingonberry.Api.UseCases.Employee.GetUsersWorkTypes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lingonberry.Api.Web.Controllers;

/// <summary>
///
/// </summary>
[ApiController]
[Route("api/employee")]
[ApiExplorerSettings(GroupName = "employee")]
[Authorize]
public class EmployeeController : ControllerBase
{
    private readonly IMediator mediator;

    /// <summary>
    /// Constructor.
    /// </summary>
    public EmployeeController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    /// <summary>
    /// Get employees by city.
    /// </summary>
    /// <param name="query">GetEmployeesByCityQuery.</param>
    /// <returns>GetEmployeesByCityResult.</returns>
    [HttpGet("getEmployeesByCity")]
    public async Task<GetEmployeesByCityResult> GetEmployeesByCity([FromQuery] GetEmployeesByCityQuery query)
    {
        return await mediator.Send(query);
    }

    /// <summary>
    /// Get divisions names by location.
    /// </summary>
    /// <returns>Divisons names collection.</returns>
    [HttpGet("locations-names")]
    public async Task<ICollection<string>> GetLocationsNames()
        => await mediator.Send(new GetLocationsNamesQuery());

    /// <summary>
    /// Get divisions names.
    /// May filtered by location name.
    /// </summary>
    /// <param name="query">Request query.</param>
    /// <returns>Divisons names collection.</returns>
    [HttpGet("divisions-names")]
    public async Task<ICollection<string>> GetDivisionsByLocation([FromQuery] GetDivisionsNamesQuery query)
    {
        return await mediator.Send(query);
    }

    /// <summary>
    /// Get departments names.
    /// May filtered by location name or divisions names.
    /// </summary>
    /// <param name="query">Request query.</param>
    /// <returns>Collection of names.</returns>
    [HttpGet("departments-names")]
    public async Task<ICollection<string?>> GetDepartments([FromQuery] GetDepartmentsNamesQuery query)
    {
        return await mediator.Send(query);
    }

    /// <summary>
    /// Get groups names.
    /// </summary>
    /// <param name="query">Contains selected location, division and department.</param>
    /// <returns>Collection of names.</returns>
    [HttpGet("groups-names")]
    public async Task<ICollection<string?>> GetGroups([FromQuery] GetGroupsNamesQuery query)
        => await mediator.Send(query);

    /// <summary>
    /// Get users positions.
    /// </summary>
    /// <param name="query">Query to get users positions.</param>
    /// <returns>Collection of positions names.</returns>
    [HttpGet("users-positions")]
    public async Task<ICollection<string?>> GetUsersPositions([FromQuery] GetUserPositionsQuery query)
        => await mediator.Send(query);

    /// <summary>
    /// Get users worktypes.
    /// </summary>
    /// <param name="query">Filters.</param>
    /// <returns>Collection of worktypes names.</returns>
    [HttpGet("users-worktypes")]
    public async Task<ICollection<string>> GetUsersWorkTypes([FromQuery] GetUsersWorkTypesQuery query)
        => await mediator.Send(query);

    /// <summary>
    /// Get user structure.
    /// </summary>
    /// <param name="query">Request query.</param>
    /// <returns>GetUserStructureResult.</returns>
    [HttpGet("user-structure")]
    public async Task<GetUserStructureResult> GetUserStructure([FromQuery] GetUserStructureQuery query)
    {
        return await mediator.Send(query);
    }

    /// <summary>
    /// Rest for get employees form table.
    /// </summary>
    /// <param name="query">GetEmployeesFormTableQuery.</param>
    /// <returns>GetEmployeesFormTableResult.</returns>
    [HttpGet("getEmployeesFormTable")]
    public async Task<GetEmployeesFormTableResult> GetEmployeesFormTable([FromQuery] GetEmployeesFormTableQuery query)
    {
        return await mediator.Send(query);
    }
}
