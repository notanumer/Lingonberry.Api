using AutoMapper;
using AutoMapper.QueryableExtensions;
using Lingonberry.Api.Infrastructure.Abstractions.Interfaces;
using Lingonberry.Api.UseCases.Structure.GetLocationData.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lingonberry.Api.UseCases.Structure.GetLocationData;

/// <summary>
/// Get division by location name handler.
/// </summary>
public class GetLocationDataQueryHandler : IRequestHandler<GetLocationDataQuery, List<GetLocationDataDto>>
{
    private readonly ILogger<GetLocationDataQueryHandler> logger;
    private readonly IAppDbContext dbContext;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    public GetLocationDataQueryHandler(ILogger<GetLocationDataQueryHandler> logger, IAppDbContext dbContext, IMapper mapper)
    {
        this.logger = logger;
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<List<GetLocationDataDto>> Handle(GetLocationDataQuery request, CancellationToken cancellationToken)
    {
        var divisions = await dbContext.Locations
            .Where(l => l.Name == request.Location)
            .SelectMany(l => l.Divisions)
            .ProjectTo<GetLocationDataDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        var groups = await dbContext.Locations
            .Where(l => l.Name == request.Location)
            .SelectMany(l => l.Groups)
            .ProjectTo<GetLocationDataDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        var departments = await dbContext.Locations
            .Where(l => l.Name == request.Location)
            .SelectMany(l => l.Departments)
            .ProjectTo<GetLocationDataDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return [.. divisions, .. departments, .. groups];
    }
}
