using AutoMapper;
using Lingonberry.Api.Domain.Locations.Helpers;
using Lingonberry.Api.Domain.Users;
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
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    public GetEmployeesByCityQueryHandler(ILogger<GetEmployeesByCityQueryHandler> logger, IAppDbContext dbContext, IMapper mapper)
    {
        this.logger = logger;
        this.dbContext = dbContext;
        this.mapper = mapper;
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

        var employerValue = new List<PositionValue>() { PositionValue.Chief, PositionValue.Director };
        var userWithoutVacancy = usersByCity.Where(x => !x.IsVacancy).ToList();
        var vacancyCount = usersByCity.Count() - userWithoutVacancy.Count;
        var userCount = userWithoutVacancy.Count;
        var em = userWithoutVacancy
            .Select(u => mapper.Map<ShortUser>(u))
            .GroupBy(x => employerValue
                .Contains(x.UserPosition)).ToList();
        var result = new GetEmployeesByCityResult
        {
            Name = request.LocationName,
            Id = usersByCity.FirstOrDefault().Location.Id,
            StructureEnum = StructureEnum.Location,
            Employers = em.Where(x => x.Key)
                .Select(x => x.ToList())
                .FirstOrDefault() ?? new List<ShortUser>(),
            Employees = em.Where(x => !x.Key)
                .Select(x => x.ToList())
                .FirstOrDefault() ?? new List<ShortUser>(),
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
                        .Select(u => mapper.Map<ShortUser>(u));
                    var emDep = shortUserDepartment
                        .GroupBy(x => employerValue
                            .Contains(x.UserPosition)).ToList();
                    var vacCount = GetVacancyCount(shortUserDepartment);
                    contentDep.UserCount = shortUserDepartment.Count() - vacCount;
                    contentDep.VacancyCount = vacCount;
                    contentDep.StructureEnum = StructureEnum.Department;
                    contentDep.Employers = emDep.Where(x => x.Key)
                            .Select(x => x.ToList())
                            .FirstOrDefault() ?? new List<ShortUser>();
                    contentDep.Employees = emDep.Where(x => !x.Key)
                        .Select(x => x.ToList())
                        .FirstOrDefault() ?? new List<ShortUser>();
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
                                .Select(u => mapper.Map<ShortUser>(u));
                            var emGroup = shortUserGroup
                                .GroupBy(x => employerValue
                                    .Contains(x.UserPosition)).ToList();
                            contents.Add(new GetEmployeesByCityResult()
                            {
                                Employers = emGroup.Where(x => x.Key)
                                    .Select(x => x.ToList())
                                    .FirstOrDefault() ?? new List<ShortUser>(),
                                Employees = emGroup.Where(x => !x.Key)
                                    .Select(x => x.ToList())
                                    .FirstOrDefault() ?? new List<ShortUser>(),
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
                                .Select(u => mapper.Map<ShortUser>(u));
                            var emUser = shortUser
                                .GroupBy(x => employerValue
                                    .Contains(x.UserPosition)).ToList();
                            contents.Add(new GetEmployeesByCityResult
                            {
                                Employers = emUser.Where(x => x.Key)
                                    .Select(x => x.ToList())
                                    .FirstOrDefault() ?? new List<ShortUser>(),
                                Employees = emUser.Where(x => !x.Key)
                                    .Select(x => x.ToList())
                                    .FirstOrDefault() ?? new List<ShortUser>(),
                                StructureEnum = StructureEnum.User,
                                Name = "Empty",
                                Id = 0,
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
                                .Select(u => mapper.Map<ShortUser>(u));
                            var emGroup = shortUserGroup
                                .GroupBy(x => employerValue
                                    .Contains(x.UserPosition)).ToList();
                            var gContent = new GetEmployeesByCityResult
                            {
                                UserCount = shortUserGroup.Count() - GetVacancyCount(shortUserGroup),
                                VacancyCount = GetVacancyCount(shortUserGroup),
                                Employers = emGroup.Where(x => x.Key)
                                    .Select(x => x.ToList())
                                    .FirstOrDefault() ?? new List<ShortUser>(),
                                Employees = emGroup.Where(x => !x.Key)
                                    .Select(x => x.ToList())
                                    .FirstOrDefault() ?? new List<ShortUser>(),
                                Name = dep.Key.Group.Name,
                                Id = dep.Key.Group.Id,
                                StructureEnum = StructureEnum.Group
                            };
                            contentDiv.Next.Add(gContent);
                        }
                        else
                        {
                            var shortUser = dep
                                .Select(u => mapper.Map<ShortUser>(u));
                            var emUser = shortUser
                                .GroupBy(x => employerValue
                                    .Contains(x.UserPosition)).ToList();
                            var gContent = new GetEmployeesByCityResult
                            {
                                UserCount = shortUser.Count() - GetVacancyCount(shortUser),
                                VacancyCount = GetVacancyCount(shortUser),
                                Employers = emUser.Where(x => x.Key)
                                    .Select(x => x.ToList())
                                    .FirstOrDefault() ?? new List<ShortUser>(),
                                Employees = emUser.Where(x => !x.Key)
                                    .Select(x => x.ToList())
                                    .FirstOrDefault() ?? new List<ShortUser>(),
                                Name = "Empty",
                                Id = 0,
                                StructureEnum = StructureEnum.User
                            };
                            contentDiv.Next.Add(gContent);
                        }
                    }
                }
            }

            if (contentDiv.Name == null)
            {
                var listDiv = contentDiv.Next;
                result.Next.AddRange(listDiv);
            }
            else
            {
                contentDiv.Employers = contentDiv.Next
                    .SelectMany(x => x.Employers).ToList();
                contentDiv.Employees = contentDiv.Next
                    .SelectMany(x => x.Employees).ToList();
                contentDiv.UserCount = contentDiv.Next.Sum(x => x.UserCount);
                contentDiv.VacancyCount = contentDiv.Next.Sum(x => x.VacancyCount);
                result.Next.Add(contentDiv);
            }
        }

        return result;
    }

    private static int GetVacancyCount(IEnumerable<ShortUser> users) => users.Count(x => x.IsVacancy);
}
