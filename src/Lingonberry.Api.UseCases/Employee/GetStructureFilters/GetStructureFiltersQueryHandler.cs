using Lingonberry.Api.Infrastructure.Abstractions.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Saritasa.Tools.Common.Extensions;

namespace Lingonberry.Api.UseCases.Employee.GetStructureFilters;

/// <summary>
/// Get structure filters handler.
/// </summary>
public class GetStructureFiltersQueryHandler : IRequestHandler<GetStructureFiltersQuery, GetStructureFiltersQueryResult>
{
    private readonly ILogger<GetStructureFiltersQueryHandler> logger;
    private readonly IAppDbContext dbContext;

    /// <summary>
    /// Constructor.
    /// </summary>
    public GetStructureFiltersQueryHandler(ILogger<GetStructureFiltersQueryHandler> logger, IAppDbContext dbContext)
    {
        this.logger = logger;
        this.dbContext = dbContext;
    }

    /// <inheritdoc />
    public Task<GetStructureFiltersQueryResult> Handle(GetStructureFiltersQuery request, CancellationToken cancellationToken)
    {
        var location = dbContext.Locations
            .Include(l => l.Divisions)
            .ThenInclude(dd => dd.Departments)
            .ThenInclude(d => d.Groups)
            .ThenInclude(d => d.Locations)
            .ThenInclude(d => d.Departments)
            .ThenInclude(d => d.Locations);

        var d = location.Select(x => x.Divisions
            .Where(y => y.Departments
                .Any(u => u.Groups
                    .Any(g => g.Locations
                        .Any(l => l.Name == x.Name)))));

        throw new Exception();
    }
}
