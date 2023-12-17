using Lingonberry.Api.UseCases.Employee.GetEmployeesByCity;
using Lingonberry.Api.UseCases.Employee.GetEmployeesFormTable;
using Lingonberry.Api.UseCases.Employee.GetStructureFilters;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Lingonberry.Api.Web.Controllers;

/// <summary>
///
/// </summary>
[ApiController]
[Route("api/employee")]
[ApiExplorerSettings(GroupName = "employee")]
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

    [HttpGet("getStructureFilters")]
    public async Task<GetStructureFiltersQueryResult> GetStructureFilters([FromQuery] GetStructureFiltersQuery query)
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
