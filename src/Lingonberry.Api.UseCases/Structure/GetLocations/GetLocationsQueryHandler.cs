using AutoMapper;
using Lingonberry.Api.Infrastructure.Abstractions.Interfaces;
using Lingonberry.Api.UseCases.Structure.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lingonberry.Api.UseCases.Structure.GetLocations;

/// <summary>
/// Get locations handler.
/// </summary>
public class GetLocationsQueryHandler : IRequestHandler<GetLocationsQuery, List<LocationDto>>
{
    private readonly ILogger<GetLocationsQueryHandler> logger;
    private readonly IAppDbContext dbContext;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    public GetLocationsQueryHandler(ILogger<GetLocationsQueryHandler> logger, IAppDbContext dbContext, IMapper mapper)
    {
        this.logger = logger;
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<List<LocationDto>> Handle(GetLocationsQuery request, CancellationToken cancellationToken)
    {
        var locations = await dbContext.Locations
            .ToListAsync(cancellationToken: cancellationToken);
        return mapper.Map<List<LocationDto>>(locations);
    }
}
