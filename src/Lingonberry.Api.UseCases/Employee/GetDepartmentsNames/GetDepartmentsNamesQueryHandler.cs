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
            var departments = await appDbContext.Departments
                .AsNoTracking()
                .Include(d => d.Locations)
                    .ThenInclude(l => l.Divisions)
                .Include(d => d.Divisions)
                .ToListAsync(cancellationToken);
            var location = await appDbContext.Locations
                .AsNoTracking()
                .Include(l => l.Divisions)
                .ThenInclude(d => d.Departments)
                .FirstOrDefaultAsync(l => l.Name == request.LocationName, cancellationToken);

            var result = new HashSet<string?>();
            if (request.DivisonNames.Any())
            {
                foreach (var name in request.DivisonNames)
                {
                    foreach (var department in departments)
                    {
                        if (department.Divisions!.Any(d => d.Name == name))
                        {
                            var division = location!.Divisions.FirstOrDefault(d => d.Name == name);
                            var divisionDepartment = division!.Departments!.FirstOrDefault(d => d.Name == department.Name);
                            if (divisionDepartment != null)
                            {
                                result.Add(divisionDepartment.Name);
                            }
                        }
                    }
                }

            }
            else
            {
                foreach (var department in departments)
                {
                    if (department.Divisions!.Any(d => d.Locations.Any(l => l.Name == request.LocationName))
                        || department.Locations.Any(l => l.Name == request.LocationName))
                    {
                        result.Add(department.Name);
                    }
                }
            }
            return result;
        }
        else
        {
            return await appDbContext.Departments
                .Select(d => d.Name)
                .ToListAsync(cancellationToken);
        }
    }
}
