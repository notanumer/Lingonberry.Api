using Lingonberry.Api.Domain.Locations.Helpers;
using Lingonberry.Api.Infrastructure.Abstractions.Interfaces;
using Lingonberry.Api.UseCases.Employee.Common;
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
            .Where(u => u.Location != null && u.Location.Name == request.LocationName)
            .Include(u => u.Division)
            .Include(u => u.Department)
            .Include(u => u.Group);

        var userWithoutVacancy = usersByCity.Where(x => !x.IsVacancy).ToList();
        var vacancyCount = usersByCity.Count() - userWithoutVacancy.Count;
        var userCount = userWithoutVacancy.Count;
        var result = new GetEmployeesByCityResult
        {
            Name = request.LocationName,
            Users = userWithoutVacancy.Select(u => new ShortUser()
            {
                Id = u.Id,
                FullName = u.FullName,
                IsVacancy = u.IsVacancy
            }).ToList(),
            UserCount = userCount,
            VacancyCount = vacancyCount
        };

        var groupDepartment = usersByCity
            .ToList()
            .GroupBy(u => new { u.Division, u.Department, u.Group })
            .GroupBy(u => u.Key.Division)
            .Select(groupDiv => groupDiv
                .GroupBy(d => d.Key.Department));

        foreach (var groupDep in groupDepartment)
        {
            var contentDiv = new GetEmployeesByCityResult();
            var curDiv = groupDep.FirstOrDefault().FirstOrDefault();
            if (curDiv?.Key.Division != null)
            {
                contentDiv.Name = curDiv.Key.Division.Name;
                contentDiv.Id = curDiv.Key.Division.Id;
                contentDiv.StructureEnum = StructureEnum.Division;
            }

            foreach (var div in groupDep)
            {
                var contentDep = new GetEmployeesByCityResult();
                if (div.FirstOrDefault()?.Key.Department != null)
                {
                    var shortUserDepartment = div
                        .SelectMany(x => x.ToList())
                        .Select(u => new ShortUser()
                        {
                            Id = u.Id,
                            FullName = u.FullName,
                            IsVacancy = u.IsVacancy
                        });
                    var vacCount = GetVacancyCount(shortUserDepartment);
                    contentDep.UserCount = shortUserDepartment.Count() - vacCount;
                    contentDep.VacancyCount = vacCount;
                    contentDep.StructureEnum = StructureEnum.Department;
                    contentDep.Users = shortUserDepartment.Where(x => !x.IsVacancy).ToList();
                    contentDep.Name = div.FirstOrDefault().Key.Department.Name;
                    contentDep.Id = div.FirstOrDefault().Key.Department.Id;
                }

                if (div.FirstOrDefault().Key.Department != null)
                {
                    var contents = new List<GetEmployeesByCityResult>();
                    foreach (var dep in div)
                    {
                        if (dep.Key.Group != null)
                        {
                            var shortUserGroup = dep
                                .Select(u => new ShortUser()
                                {
                                    Id = u.Id,
                                    FullName = u.FullName,
                                    IsVacancy = u.IsVacancy
                                });
                            contents.Add(new GetEmployeesByCityResult()
                            {
                                Users = shortUserGroup.Where(u => !u.IsVacancy).ToList(),
                                Name = dep.Key.Group.Name,
                                Id = dep.Key.Group.Id,
                                StructureEnum = StructureEnum.Group,
                                UserCount = shortUserGroup.Count() - GetVacancyCount(shortUserGroup),
                                VacancyCount = GetVacancyCount(shortUserGroup)
                            });
                        }
                        else
                        {
                            var shortUser = dep
                                .Select(u => new ShortUser()
                                {
                                    Id = u.Id,
                                    FullName = u.FullName,
                                    IsVacancy = u.IsVacancy
                                });
                            contents.Add(new GetEmployeesByCityResult
                            {
                                Users = shortUser.Where(u => !u.IsVacancy).ToList(),
                                StructureEnum = StructureEnum.User,
                                Name = "Empty",
                                UserCount = shortUser.Count() - GetVacancyCount(shortUser),
                                VacancyCount = GetVacancyCount(shortUser)
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
                            var shortUserGroup = dep
                                .Select(u => new ShortUser()
                                {
                                    Id = u.Id,
                                    FullName = u.FullName,
                                    IsVacancy = u.IsVacancy
                                });
                            var gContent = new GetEmployeesByCityResult
                            {
                                UserCount = shortUserGroup.Count() - GetVacancyCount(shortUserGroup),
                                VacancyCount = GetVacancyCount(shortUserGroup),
                                Users = shortUserGroup.Where(u => !u.IsVacancy).ToList(),
                                Name = dep.Key.Group.Name,
                                Id = dep.Key.Group.Id,
                                StructureEnum = StructureEnum.Group
                            };
                            contentDiv.Next.Add(gContent);
                        }
                        else
                        {
                            var shortUser = dep
                                .Select(u => new ShortUser()
                                {
                                    Id = u.Id,
                                    FullName = u.FullName,
                                    IsVacancy = u.IsVacancy
                                });
                            var gContent = new GetEmployeesByCityResult
                            {
                                UserCount = shortUser.Count() - GetVacancyCount(shortUser),
                                VacancyCount = GetVacancyCount(shortUser),
                                Users = shortUser.Where(x => !x.IsVacancy).ToList(),
                                Name = "Empty",
                                StructureEnum = StructureEnum.User
                            };
                            contentDiv.Next.Add(gContent);
                        }
                    }
                }
            }
            contentDiv.Users = contentDiv.Next.SelectMany(x => x.Users).ToList();
            contentDiv.UserCount = contentDiv.Next.Sum(x => x.UserCount);
            contentDiv.VacancyCount = contentDiv.Next.Sum(x => x.VacancyCount);
            result.Next.Add(contentDiv);
        }

        return result;
    }

    private static int GetVacancyCount(IEnumerable<ShortUser> users) => users.Count(x => x.IsVacancy);
}
