using Lingonberry.Api.Domain.Users;
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
            .Where(u => u.Location.Name == request.LocationName)
            .Include(u => u.Department)
            .Include(u => u.Division)
            .Include(u => u.Group)
            .AsQueryable();

        if (request.DivisionName != null)
        {
            usersByCity = usersByCity.Where(u => u.Division.Name == request.DivisionName);
        }

        if (request.DepartmentName != null)
        {
            usersByCity = usersByCity.Where(u => u.Department.Name == request.DepartmentName);
        }

        if (request.GroupName != null)
        {
            usersByCity = usersByCity.Where(u => u.Group.Name == request.GroupName);
        }

        if (request.SalaryFilterOrder != null)
        {

        }

        var userWithoutVacancy = usersByCity.Where(x => !x.IsVacancy).ToList();
        var vacancyCount = usersByCity.Count() - userWithoutVacancy.Count;
        var userCount = userWithoutVacancy.Count;
        var result = new GetEmployeesByCityResult
        {
            Response = new Content
            {
                Name = request.LocationName,
                Users = userWithoutVacancy,
                UserCount = userCount,
                VacancyCount = vacancyCount
            }
        };

        var groupDepartment = usersByCity
            .ToList()
            .GroupBy(u => new { u.Division, u.Department, u.Group })
            .GroupBy(u => u.Key.Division)
            .Select(groupDiv => groupDiv
                .GroupBy(d => d.Key.Department));

        foreach (var groupDep in groupDepartment)
        {
            var contentDiv = new Content();
            var curDiv = groupDep.FirstOrDefault().FirstOrDefault();
            if (curDiv.Key.Division != null)
            {
                contentDiv.Name = curDiv.Key.Division.Name;
            }

            foreach (var div in groupDep)
            {
                var contentDep = new Content();
                if (div.FirstOrDefault().Key.Department != null)
                {
                    var usersDep = div.SelectMany(x => x.ToList()).ToList();
                    var vacCount = GetVacancyCount(usersDep);
                    contentDep.UserCount = usersDep.Count - vacCount;
                    contentDep.VacancyCount = vacCount;
                    contentDep.Users = usersDep.Where(x => !x.IsVacancy).ToList();
                    contentDep.Name = div.FirstOrDefault().Key.Department.Name;
                }

                if (div.FirstOrDefault().Key.Department != null)
                {
                    var contents = new List<Content>();
                    foreach (var dep in div)
                    {
                        if (dep.Key.Group != null)
                        {
                            contents.Add(new Content()
                            {
                                Users = dep.Where(x => !x.IsVacancy).ToList(),
                                Name = dep.Key.Group.Name,
                                UserCount = dep.ToList().Count - GetVacancyCount(dep.ToList()),
                                VacancyCount = GetVacancyCount(dep.ToList())
                            });
                        }
                        else
                        {
                            contents.Add(new Content()
                            {
                                Users = dep.Where(x => !x.IsVacancy).ToList(),
                                Name = "Empty",
                                UserCount = dep.ToList().Count - GetVacancyCount(dep.ToList()),
                                VacancyCount = GetVacancyCount(dep.ToList())
                            });
                        }
                    }
                    if (contents.Any())
                    {
                        contentDep.Next.AddRange(contents);
                    }
                    contentDiv.Next.Add(contentDep);
                }
                else
                {
                    foreach (var dep in div)
                    {
                        if (dep.Key.Group != null)
                        {
                            var gContent = new Content
                            {
                                UserCount = dep.ToList().Count - GetVacancyCount(dep.ToList()),
                                VacancyCount = GetVacancyCount(dep.ToList()),
                                Users = dep.Where(x => !x.IsVacancy).ToList(),
                                Name = dep.Key.Group.Name
                            };
                            contentDiv.Next.Add(gContent);
                        }
                        else
                        {
                            var gContent = new Content
                            {
                                UserCount = dep.ToList().Count - GetVacancyCount(dep.ToList()),
                                VacancyCount = GetVacancyCount(dep.ToList()),
                                Users = dep.Where(x => !x.IsVacancy).ToList(),
                                Name = "Empty"
                            };
                            contentDiv.Next.Add(gContent);
                        }
                    }
                }
            }
            var users = contentDiv.Next.SelectMany(x => x.Users);
            contentDiv.Users = users.Where(x => !x.IsVacancy).ToList();
            contentDiv.UserCount = contentDiv.Users.Count;
            contentDiv.VacancyCount = GetVacancyCount(users.ToList());
            result.Response.Next.Add(contentDiv);
        }

        return result;
    }

    private int GetVacancyCount(ICollection<User> users) => users.Count(x => x.IsVacancy);
}
