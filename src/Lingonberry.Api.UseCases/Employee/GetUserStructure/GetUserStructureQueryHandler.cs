using AutoMapper;
using Lingonberry.Api.Domain.Locations.Helpers;
using Lingonberry.Api.Domain.Users;
using Lingonberry.Api.Infrastructure.Abstractions.Interfaces;
using Lingonberry.Api.UseCases.Employee.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Saritasa.Tools.EntityFrameworkCore;

namespace Lingonberry.Api.UseCases.Employee.GetUserStructure;

internal class GetUserStructureQueryHandler : IRequestHandler<GetUserStructureQuery, GetUserStructureResult>
{
    private readonly IAppDbContext appDbContext;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    public GetUserStructureQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        this.appDbContext = appDbContext;
        this.mapper = mapper;
    }

    public async Task<GetUserStructureResult> Handle(GetUserStructureQuery request, CancellationToken cancellationToken)
    {
        var user = await appDbContext.Users
            .Include(u => u.Location)
            .Include(u => u.Division)
            .Include(u => u.Department)
            .Include(u => u.Group)
            .GetAsync(u => u.Id == request.UserId, cancellationToken: cancellationToken);

        var result = new GetUserStructureResult();
        var inserter = result;

        var employerValue = new List<PositionValue>() { PositionValue.Head, PositionValue.Director };

        inserter.Name = user.Location!.Name;
        inserter.StructureEnum = StructureEnum.Location;
        inserter.Next = new GetUserStructureResult();
        inserter = inserter.Next;

        if (user.Division != null)
        {
            inserter.Name = user.Division.Name;
            inserter.Id = user.DivisionId;
            inserter.StructureEnum = StructureEnum.Division;
            inserter.Next = new GetUserStructureResult();
            inserter = inserter.Next;
        }

        if (user.Department != null)
        {
            inserter.Name = user.Department.Name;
            inserter.Id = user.DepartmentId;
            inserter.StructureEnum = StructureEnum.Department;
            inserter.Next = new GetUserStructureResult();
            inserter = inserter.Next;
        }

        if (user.Group != null)
        {
            inserter.Name = user.Group.Name;
            inserter.Id = user.GroupId;
            inserter.StructureEnum = StructureEnum.Group;
        }

        switch (inserter.StructureEnum)
        {
            case StructureEnum.Division:
                var division = await appDbContext.Divisions
                    .Include(g => g.Users)
                        .ThenInclude(u => u.Location)
                    .GetAsync(d => d.Id == user.DivisionId, cancellationToken);
                var employerDiv = division.Users
                    .Where(u => u.Location!.Name == user.Location.Name)
                    .Select(u => mapper.Map<ShortUser>(u))
                    .GroupBy(x => employerValue
                        .Contains(x.UserPosition)).ToList();
                inserter.Employers = employerDiv.Where(x => x.Key)
                    .Select(x => x.ToList())
                    .FirstOrDefault() ?? new List<ShortUser>();
                inserter.Employees = employerDiv.Where(x => !x.Key)
                    .Select(x => x.ToList())
                    .FirstOrDefault() ?? new List<ShortUser>();
                break;
            case StructureEnum.Department:
                var department = await appDbContext.Departments
                    .Include(g => g.Users)
                        .ThenInclude(u => u.Location)
                    .GetAsync(d => d.Id == user.DepartmentId, cancellationToken);
                var employerDep = department.Users
                    .Where(u => u.Location!.Name == user.Location.Name)
                    .Select(u => mapper.Map<ShortUser>(u))
                    .GroupBy(x => employerValue
                        .Contains(x.UserPosition)).ToList();
                inserter.Employers = employerDep.Where(x => x.Key)
                    .Select(x => x.ToList())
                    .FirstOrDefault() ?? new List<ShortUser>();
                inserter.Employees = employerDep.Where(x => !x.Key)
                    .Select(x => x.ToList())
                    .FirstOrDefault() ?? new List<ShortUser>();
                break;
            case StructureEnum.Group:
                var group = await appDbContext.Groups
                    .Include(g => g.Users)
                        .ThenInclude(u => u.Location)
                    .GetAsync(g => g.Id == user.GroupId, cancellationToken);
                var employerGroup = group.Users
                    .Where(u => u.Location!.Name == user.Location.Name)
                    .Select(u => mapper.Map<ShortUser>(u))
                    .GroupBy(x => employerValue
                        .Contains(x.UserPosition)).ToList();
                inserter.Employers = employerGroup.Where(x => x.Key)
                    .Select(x => x.ToList())
                    .FirstOrDefault() ?? new List<ShortUser>();
                inserter.Employees = employerGroup.Where(x => !x.Key)
                    .Select(x => x.ToList())
                    .FirstOrDefault() ?? new List<ShortUser>();
                break;
        }

        return result;
    }
}
