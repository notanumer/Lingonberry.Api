using AutoMapper;
using Lingonberry.Api.Infrastructure.Abstractions.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Lingonberry.Api.UseCases.Employee.GetDepartments;

internal class GetDepartmentsQueryHandler : IRequestHandler<GetDepartmentsQuery, GetDepartmentsQueryResult>
{
    private readonly IAppDbContext appDbContext;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    public GetDepartmentsQueryHandler(IMapper mapper, IAppDbContext appDbContext)
    {
        this.mapper = mapper;
        this.appDbContext = appDbContext;
    }

    public async Task<GetDepartmentsQueryResult> Handle(GetDepartmentsQuery request, CancellationToken cancellationToken)
    {
        var departmentsByLocation = await appDbContext.Departments
            .Where(d => d.DivisionId == null)
            .Where(d => d.Locations.Where(l => l.Name == request.LocationName).Any())
            .ToListAsync(cancellationToken);

        var test = await appDbContext.Locations
            .Include(l => l.Departments)
            .FirstOrDefaultAsync(l => l.Name == request.LocationName, cancellationToken);

        return new GetDepartmentsQueryResult();
    }
}
