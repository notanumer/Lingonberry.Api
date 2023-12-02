using Lingonberry.Api.Domain.Locations;
using Lingonberry.Api.Infrastructure.Abstractions.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lingonberry.Api.UseCases.Employee.GetEmployeesByCity;

/// <summary>
/// Get employees by city handler.
/// </summary>
public class GetEmployeesByCityQueryHandler : IRequestHandler<GetEmployeesByCityQuery, GetEmployeesByCityResult>
{
    private readonly ILogger<GetEmployeesByCityQueryHandler> logger;
    private readonly IAppDbContext dbContext;

    /// <summary>
    /// Constructor.
    /// </summary>
    public GetEmployeesByCityQueryHandler(ILogger<GetEmployeesByCityQueryHandler> logger, IAppDbContext dbContext)
    {
        this.logger = logger;
        this.dbContext = dbContext;
    }


    /// <inheritdoc />
    public async Task<GetEmployeesByCityResult> Handle(GetEmployeesByCityQuery request, CancellationToken cancellationToken)
    {
        var usersByCity = dbContext.Users
            .Include(x => x.Location)
            .Where(x => x.Location!.Name == request.CityName)
            .Include(u => u.Department)
            .Include(u => u.Division)
            .Include(u => u.Group)
            .AsQueryable();

        if (request.SalaryFilterOrder != null)
        {

        }

        //var usersPaged = PagedListFactory.FromSource(usersByCity, page: request.Page, pageSize: request.PageSize);
        var vacancyCount = usersByCity.Count(x => x.IsVacancy);
        var userCount = usersByCity.Count() - vacancyCount;
        var result = new GetEmployeesByCityResult
        {
            Result = new List<LinkedList<List<LinkedList<List<BaseDomain>>>>>(),
            VacancyCount = vacancyCount,
            UserCount = userCount
        };

        var divisions = usersByCity
            .Where(u => !u.IsVacancy)
            .ToList()
            .GroupBy(u => new { u.Division, u.Department, u.Group })
            .GroupBy(x => x.Key.Division)
            .Select(x => x
                .GroupBy(x => x.Key.Department));

        foreach (var division in divisions)
        {
            var di = new LinkedList<List<LinkedList<List<BaseDomain>>>>();
            var divLinkedList = new List<LinkedList<List<BaseDomain>>>();
            var divLL = new LinkedList<List<BaseDomain>>();
            var curDiv = division.FirstOrDefault().FirstOrDefault();
            if (curDiv.Key.Division != null)
            {
                divLL.AddLast(new List<BaseDomain>()
                {
                    new() { Users = curDiv.ToList(), Name = curDiv.Key.Division.Name }
                });
                divLinkedList.Add(divLL);
                di.AddLast(divLinkedList);
            }
            var depLinkedList = new List<LinkedList<List<BaseDomain>>>();
            foreach (var div in division)
            {
                var depList = new LinkedList<List<BaseDomain>>();
                if (div.FirstOrDefault().Key.Department != null)
                {
                    depList.AddLast(new List<BaseDomain>()
                    {
                        new() { Name = div.FirstOrDefault().Key.Department.Name }
                    });
                    depLinkedList.Add(depList);
                }

                var g = new List<BaseDomain>();
                if (div.FirstOrDefault().Key.Department != null)
                {
                    foreach (var dep in div)
                    {
                        if (dep.Key.Group != null)
                        {
                            g.Add(new BaseDomain { Users = dep.ToList(), Name = dep.Key.Group.Name });
                        }
                    }

                    if (g.Any())
                    {
                        depList.AddLast(g);
                    }
                }
                else
                {
                    foreach (var dep in div)
                    {
                        if (dep.Key.Group != null)
                        {
                            var depLL = new LinkedList<List<BaseDomain>>();
                            depLL.AddLast(new List<BaseDomain>()
                            {
                                new() { Users = dep.ToList(), Name = dep.Key.Group.Name }
                            });
                            depLinkedList.Add(depLL);
                        }
                        else
                        {
                            var depLL = new LinkedList<List<BaseDomain>>();
                            depLL.AddLast(new List<BaseDomain>()
                            {
                                new() { Users = dep.ToList(), Name = "Empty" }
                            });
                            depLinkedList.Add(depLL);
                        }
                    }
                }
            }
            di.AddLast(depLinkedList);
            result.Result.Add(di);
        }

        return result;
    }
}
