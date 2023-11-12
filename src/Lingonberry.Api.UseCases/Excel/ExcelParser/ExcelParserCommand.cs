using MediatR;
using Microsoft.AspNetCore.Http;

namespace Lingonberry.Api.UseCases.Excel.ExcelParser;

/// <summary>
///
/// </summary>
public record ExcelParserCommand : IRequest
{
    /// <summary>
    /// Excel file.
    /// </summary>
    required public IFormFile File { get; init; }
}
