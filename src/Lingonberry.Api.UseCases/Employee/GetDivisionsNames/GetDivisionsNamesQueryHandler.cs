using AutoMapper;
using AutoMapper.QueryableExtensions;
using Lingonberry.Api.Domain.Locations;
using Lingonberry.Api.Infrastructure.Abstractions.Interfaces;
using Lingonberry.Api.UseCases.Employee.GetDivisionsNames.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lingonberry.Api.UseCases.Employee.GetDivisionsNames;

/// <summary>
/// Get structure filters handler.
/// </summary>
public class GetDivisionsNamesQueryHandler : IRequestHandler<GetDivisionsNamesQuery, ICollection<string>>
{
    private readonly ILogger<GetDivisionsNamesQueryHandler> logger;
    private readonly IAppDbContext dbContext;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    public GetDivisionsNamesQueryHandler(ILogger<GetDivisionsNamesQueryHandler> logger, IAppDbContext dbContext, IMapper mapper)
    {
        this.logger = logger;
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<ICollection<string>> Handle(GetDivisionsNamesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            IQueryable<Division> divisions;
            if (!string.IsNullOrEmpty(request.LocationName))
            {
                divisions = dbContext.Locations
                    .Where(l => l.Name == request.LocationName)
                    .SelectMany(l => l.Divisions);
            }
            else
            {
                divisions = dbContext.Divisions;
            }

            return await divisions
                    .ProjectTo<GetDivisionsNamesDto>(mapper.ConfigurationProvider)
                    .Select(l => l.Name)
                    .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error on getting divisions by location name {LocationName}", request.LocationName);
            return new List<string>();
        }
    }
}
