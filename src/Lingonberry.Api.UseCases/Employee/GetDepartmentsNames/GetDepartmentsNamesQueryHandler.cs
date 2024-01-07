using Lingonberry.Api.Infrastructure.Abstractions.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Saritasa.Tools.Common.Extensions;

namespace Lingonberry.Api.UseCases.Employee.GetDepartmentsNames;

internal class GetDepartmentsNamesQueryHandler : IRequestHandler<GetDepartmentsNamesQuery, ICollection<string?>>
{
    private readonly IAppDbContext appDbContext;

    /// <summary>
    /// Constructor.
    /// </summary>
    public GetDepartmentsNamesQueryHandler(IAppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public async Task<ICollection<string?>> Handle(GetDepartmentsNamesQuery request, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrEmpty(request.LocationName))
        {
            var location = await appDbContext.Locations
                .AsNoTracking()
                .Include(l => l.Divisions)
                .ThenInclude(d => d.Departments)
                .Include(l => l.Departments)
                .FirstOrDefaultAsync(l => l.Name == request.LocationName, cancellationToken);

            if (!string.IsNullOrEmpty(request.DivisonName))
            {
                var division = location?.Divisions
                    .FirstOrDefault(d => d.Name == request.DivisonName);
                if (division != null)
                {
                    return division.Departments!.Select(d => d.Name).ToList();
                }
                else
                {
                    return new List<string?>();
                }
            }
            else
            {
                return location!.Departments.Select(d => d.Name).ToList();
            }
        }
        else
        {
            return await appDbContext.Departments
                .Select(d => d.Name)
                .ToListAsync(cancellationToken);
        }
    }
}
