using AutoMapper;
using Lingonberry.Api.Infrastructure.Abstractions.Interfaces;
using Lingonberry.Api.UseCases.Structure.Common;
using Lingonberry.Api.UseCases.Structure.GetLocations;
using MediatR;
using Microsoft.Extensions.Logging;
using Saritasa.Tools.EntityFrameworkCore;

namespace Lingonberry.Api.UseCases.Structure.GetLocationById;

/// <summary>
/// Get location by id.
/// </summary>
public class GetLocationByIdQueryHandler : IRequestHandler<GetLocationByIdQuery, LocationDto>
{
    private readonly ILogger<GetLocationByIdQueryHandler> logger;
    private readonly IAppDbContext dbContext;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    public GetLocationByIdQueryHandler(ILogger<GetLocationByIdQueryHandler> logger, IAppDbContext dbContext, IMapper mapper)
    {
        this.logger = logger;
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<LocationDto> Handle(GetLocationByIdQuery request, CancellationToken cancellationToken)
    {
        var location = await dbContext.Locations.GetAsync(l => l.Id == request.Id, cancellationToken);
        return mapper.Map<LocationDto>(location);
    }
}
