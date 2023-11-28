using Lingonberry.Api.UseCases.Employee.GetEmployeesByCity;
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
}
