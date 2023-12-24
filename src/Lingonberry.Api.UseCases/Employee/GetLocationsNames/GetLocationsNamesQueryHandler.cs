using Lingonberry.Api.Infrastructure.Abstractions.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Lingonberry.Api.UseCases.Employee.GetLocationsNames;

/// <summary>
/// Handler for <see cref="GetLocationsNamesQuery"/>.
/// </summary>
internal class GetLocationsNamesQueryHandler : IRequestHandler<GetLocationsNamesQuery, ICollection<string>>
{
    private readonly IAppDbContext appDbContext;

    /// <summary>
    /// Constructor.
    /// </summary>
    public GetLocationsNamesQueryHandler(IAppDbContext appDbContext) => this.appDbContext = appDbContext;

    public async Task<ICollection<string>> Handle(GetLocationsNamesQuery request, CancellationToken cancellationToken)
    {
        return await appDbContext.Locations.Select(l => l.Name).ToListAsync(cancellationToken);
    }
}
