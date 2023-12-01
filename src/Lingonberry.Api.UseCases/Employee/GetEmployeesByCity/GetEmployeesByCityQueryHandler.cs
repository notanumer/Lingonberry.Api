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
    public Task<GetEmployeesByCityResult> Handle(GetEmployeesByCityQuery request, CancellationToken cancellationToken)
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

        var result = new GetEmployeesByCityResult { Result = new List<LinkedList<BaseDomain>>() };

        AddBaseDomain(usersByCity, Location.Division);

        void AddBaseDomain(IEnumerable<User> baseDomain, Location location)
        {
            IEnumerable<IGrouping<BaseDomain?, User>> domains = new List<IGrouping<BaseDomain?, User>>();

            switch (location)
            {
                case Location.Division:
                    domains = baseDomain.GroupBy(u => u.Division);
                    break;
                case Location.Department:
                    domains = baseDomain.GroupBy(u => u.Department);
                    break;
                case Location.Group:
                    domains = baseDomain.GroupBy(u => u.Group);
                    break;
            }

            foreach (var domain in domains)
            {
                var linkedList = new LinkedList<BaseDomain>();
                if (domain.Key != null)
                {
                    linkedList.AddLast(domain.Key);
                    result.Result.Add(linkedList);
                }
                else
                {
                    var l = (int)location + 1;
                    if (l == 3)
                    {
                        break;
                    }
                    AddBaseDomain(domain, Enum.GetValues<Location>()[l]);
                }
            }
        }
        var usersPaged = PagedListFactory.FromSource(usersByCity, page: request.Page, pageSize: request.PageSize);

        throw new Exception();
    }
}
