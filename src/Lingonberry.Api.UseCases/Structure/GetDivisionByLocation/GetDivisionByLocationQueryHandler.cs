using AutoMapper;
using Lingonberry.Api.Infrastructure.Abstractions.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lingonberry.Api.UseCases.Structure.GetDivisionByLocation;

/// <summary>
/// Get division by location name handler.
/// </summary>
public class GetDivisionByLocationQueryHandler : IRequestHandler<GetDivisionByLocationQuery, List<GetDivisionByLocationQueryResult>>
{
    private readonly ILogger<GetDivisionByLocationQueryHandler> logger;
    private readonly IAppDbContext dbContext;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    public GetDivisionByLocationQueryHandler(ILogger<GetDivisionByLocationQueryHandler> logger, IAppDbContext dbContext, IMapper mapper)
    {
        this.logger = logger;
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<List<GetDivisionByLocationQueryResult>> Handle(GetDivisionByLocationQuery request, CancellationToken cancellationToken)
    {
        var division = dbContext.Divisions
            .Include(d => d.Locations)
            .Where(d => d.Locations.FirstOrDefault(l => l.Name == request.Location)!.Name == request.Location)
            .Select(d => mapper.Map<GetDivisionByLocationQueryResult>(d));

        return await division.ToListAsync(cancellationToken: cancellationToken);
    }
}
