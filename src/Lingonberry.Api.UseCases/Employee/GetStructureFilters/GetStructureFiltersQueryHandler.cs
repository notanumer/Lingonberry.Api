using AutoMapper;
using AutoMapper.QueryableExtensions;
using Lingonberry.Api.Infrastructure.Abstractions.Interfaces;
using Lingonberry.Api.UseCases.Employee.GetStructureFilters.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lingonberry.Api.UseCases.Employee.GetStructureFilters;

/// <summary>
/// Get structure filters handler.
/// </summary>
public class GetStructureFiltersQueryHandler : IRequestHandler<GetStructureFiltersQuery, GetStructureFiltersQueryResult>
{
    private readonly ILogger<GetStructureFiltersQueryHandler> logger;
    private readonly IAppDbContext dbContext;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    public GetStructureFiltersQueryHandler(ILogger<GetStructureFiltersQueryHandler> logger, IAppDbContext dbContext, IMapper mapper)
    {
        this.logger = logger;
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<GetStructureFiltersQueryResult> Handle(GetStructureFiltersQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var divisons = await dbContext.Locations
            .Where(l => l.Name == request.LocationName)
            .SelectMany(l => l.Divisions)
            .ProjectTo<DivisionByLocationDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

            return new GetStructureFiltersQueryResult { Divisions = divisons };
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error on getting divisions by location name {LocationName}", request.LocationName);
            return new GetStructureFiltersQueryResult();
        }
    }
}
