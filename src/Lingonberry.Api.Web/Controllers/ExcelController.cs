using System.Net;
using System.Runtime.InteropServices;
using Lingonberry.Api.UseCases.Excel.ExcelParser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lingonberry.Api.Web.Controllers;

/// <summary>
/// Excel controller.
/// </summary>
[ApiController]
[Route("api/excel")]
[ApiExplorerSettings(GroupName = "excel")]
public class ExcelController : ControllerBase
{
    private readonly IMediator mediator;

    /// <summary>
    /// Constructor.
    /// </summary>
    public ExcelController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost("parser")]
    public async Task<IActionResult> ParseExcelFile([FromForm] ExcelParserCommand request)
    {
        await mediator.Send(request);
        return Ok();
    }
}
