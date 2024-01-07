using Lingonberry.Api.Infrastructure.Abstractions.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Saritasa.Tools.Common.Extensions;
using Saritasa.Tools.EntityFrameworkCore;

namespace Lingonberry.Api.UseCases.Employee.GetGroupsNames;

/// <summary>
/// Handler for <see cref="GetGroupsNamesQuery"/>.
/// </summary>
/// <remarks>
/// Constructor.
/// </remarks>
/// <param name="appDbContext"></param>
internal class GetGroupsNamesQueryHandler(IAppDbContext appDbContext) : IRequestHandler<GetGroupsNamesQuery, ICollection<string?>>
{
    private readonly IAppDbContext appDbContext = appDbContext;

    /// <inheritdoc/>
    public async Task<ICollection<string?>> Handle(GetGroupsNamesQuery request, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrEmpty(request.LocationName))
        {
            var locationWithDivision = await appDbContext.Locations
                    .AsNoTracking()
                    .Include(l => l.Divisions)
                    .ThenInclude(d => d.Groups)
                    .Include(l => l.Departments)
                    .GetAsync(l => l.Name == request.LocationName, cancellationToken);
            if (!string.IsNullOrEmpty(request.DivisionName))
            {
                var result = new HashSet<string?>();
                if (!string.IsNullOrEmpty(request.DepartmentName))
                {
                    foreach (var division in locationWithDivision.Divisions.Where(d => d.Name == request.DivisionName))
                    {
                        foreach (var department in division.Departments!.Where(d => d.Name == request.DepartmentName))
                        {
                            result.Add(department.Groups!.Select(g => g.Name));
                        }
                    }

                    return result;
                }

                foreach (var division in locationWithDivision.Divisions.Where(d => d.Name == request.DivisionName))
                {
                    result.Add(division.Groups!.Select(g => g.Name));
                }
                return result;
            }

            if (!string.IsNullOrEmpty(request.DepartmentName))
            {
                var result = new HashSet<string?>();
                foreach (var department in locationWithDivision.Departments.Where(d => d.Name == request.DepartmentName))
                {
                    foreach (var group in department.Groups!)
                    {
                        result.Add(group.Name);
                    }
                }
                return result;
            }

            var location = await appDbContext.Locations
                .AsNoTracking()
                .Include(l => l.Groups)
                .GetAsync(l => l.Name == request.LocationName, cancellationToken);
            return location.Groups.Select(g => g.Name).ToList();
        }
        else
        {
            return await appDbContext.Groups.Select(g => g.Name).ToListAsync(cancellationToken);
        }
    }
}
