using Lingonberry.Api.UseCases.Employee.GetEmployeesByCity;
using Lingonberry.Api.UseCases.Employee.GetEmployeesFormTable;
using Lingonberry.Api.UseCases.Employee.GetStructureFilters;
using Lingonberry.Api.UseCases.Employee.GetDepartments;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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

    /// <summary>
    /// Get divisions by location.
    /// </summary>
    /// <param name="query">Request query.</param>
    /// <returns>Divisons names collection.</returns>
    [HttpGet("divisions-by-location")]
    public async Task<GetStructureFiltersQueryResult> GetDivisionsByLocation([FromQuery] GetStructureFiltersQuery query)
    {
        return await mediator.Send(query);
    }

    [HttpGet("departments-by-location-and-divison")]
    public async Task<GetDepartmentsQueryResult> GetDepartments([FromQuery] GetDepartmentsQuery query)
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
