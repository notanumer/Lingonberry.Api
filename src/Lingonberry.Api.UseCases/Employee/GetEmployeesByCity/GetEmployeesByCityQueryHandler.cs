using Lingonberry.Api.Domain.Locations;
using Lingonberry.Api.Domain.Locations.Helpers;
using Lingonberry.Api.Domain.Users;
using Lingonberry.Api.Infrastructure.Abstractions.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Saritasa.Tools.Common.Pagination;

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

        if (request.Gender != null)
        {
            switch (request.Gender)
            {
                case Gender.Male:
                    break;
                case Gender.Female:
                    break;
            }
        }

        if (request.DepartmentSorted != null)
        {
            switch (request.DepartmentSorted)
            {
                case SortOrder.Ascending:
                    var d =
                    usersByCity = usersByCity.OrderBy(x => x.Department.Name);
                    break;
                case SortOrder.Descending:
                    usersByCity = usersByCity.OrderByDescending(x => x.Department.Name);
                    break;
            }
        }

        if (request.DivisionsSorted != null)
        {
            switch (request.DivisionsSorted)
            {
                case SortOrder.Ascending:
                    usersByCity = usersByCity.OrderBy(x => x.Division.Name);
                    break;
                case SortOrder.Descending:
                    usersByCity = usersByCity.OrderByDescending(x => x.Division.Name);
                    break;
            }
        }

        if (request.GroupsSorted != null)
        {
            switch (request.GroupsSorted)
            {
                case SortOrder.Ascending:
                    usersByCity = usersByCity.OrderBy(x => x.Group.Name);
                    break;
                case SortOrder.Descending:
                    usersByCity = usersByCity.OrderByDescending(x => x.Group.Name);
                    break;
            }
        }

        if (request.SalaryFilterOrder != null)
        {

        }

        if (request.LocationsSorted != null)
        {
            switch (request.LocationsSorted)
            {
                case SortOrder.Ascending:
                    usersByCity = usersByCity.OrderBy(x => x.Location);
                    break;
                case SortOrder.Descending:
                    usersByCity = usersByCity.OrderByDescending(x => x.Location);
                    break;
            }
        }

        var usersPaged = PagedListFactory.FromSource(usersByCity, page: request.Page, pageSize: request.PageSize);
        var vacancyCount = usersByCity.Count(x => x.IsVacancy);
        var userCount = usersByCity.Count() - vacancyCount;
        var result = new GetEmployeesByCityResult
        {
            Result = new List<List<LinkedList<BaseDomain>>>(),
            VacancyCount = vacancyCount,
            UserCount = userCount
        };

        var divisions = usersByCity
            .Where(u => !u.IsVacancy)
            .ToList()
            .GroupBy(u => new { u.Division, u.Department, u.Group })
            .GroupBy(x => x.Key.Division);


        foreach (var division in divisions)
        {
            var list = new List<LinkedList<BaseDomain>>();
            foreach (var div in division)
            {
                var linkedList = new LinkedList<BaseDomain>();
                if (div.Key.Division != null)
                {
                    linkedList.AddLast(new BaseDomain() { Users = div.ToList(), Name = div.Key.Division.Name });
                }
                if (div.Key.Department != null)
                {
                    linkedList.AddLast(new BaseDomain() { Users = div.ToList(), Name = div.Key.Department.Name });
                }
                if (div.Key.Group != null)
                {
                    linkedList.AddLast(new BaseDomain() { Users = div.ToList(), Name = div.Key.Group.Name });
                }

                if (div.Key.Division == null && div.Key.Department == null && div.Key.Group == null)
                {
                    linkedList.AddLast(new BaseDomain() { Users = div.ToList(), Name = "Location" });
                }
                list.Add(linkedList);
            }
            result.Result.Add(list);
        }

        return result;
    }
}
