using Lingonberry.Api.UseCases.Employee.GetEmployeesByCity;
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
    public async Task<string> GetEmployeesByCity([FromQuery] GetEmployeesByCityQuery query)
    {
        var settings = new JsonSerializerSettings
        {
            PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            Formatting = Formatting.Indented
        };
        var d = await mediator.Send(query);
        return JsonConvert.SerializeObject(d, settings);
    }

    [HttpGet("getStructureFilters")]
    public async Task<string> GetStructureFilters([FromQuery] GetEmployeesByCityQuery query)
    {
        var settings = new JsonSerializerSettings
        {
            PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            Formatting = Formatting.Indented
        };
        var d = await mediator.Send(query);
        return JsonConvert.SerializeObject(d, settings);
    }
}
