using ClosedXML.Excel;
using Lingonberry.Api.Infrastructure.Abstractions.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Lingonberry.Api.UseCases.Excel.ExcelParser;

/// <summary>
/// Excel parser.
/// </summary>
public class ExcelParserCommandHandler : IRequestHandler<ExcelParserCommand>
{
    private readonly ILogger<ExcelParserCommandHandler> logger;
    private readonly IAppDbContext dbContext;

    /// <summary>
    /// Constructor.
    /// </summary>
    public ExcelParserCommandHandler(ILogger<ExcelParserCommandHandler> logger, IAppDbContext dbContext)
    {
        this.logger = logger;
        this.dbContext = dbContext;
    }


    /// <inheritdoc />
    public async Task Handle(ExcelParserCommand request, CancellationToken cancellationToken)
    {
        using var stream = new MemoryStream();
        await request.File.CopyToAsync(stream, cancellationToken);
        using var wbook = new XLWorkbook(stream);
        var ws = wbook.Worksheet(1);
        var countRow = ws.Rows().Count();
        for (var i = 3; i <= countRow; i++)
        {
            var d = ws.Range($"C{i}:I{i}");
        }
    }
}
