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
        var inserter = new List<GetUserStructureResult>();

        var employerValue = new List<PositionValue>() { PositionValue.Head, PositionValue.Director };

        result.Name = user.Location!.Name;
        result.StructureEnum = StructureEnum.Location;
        result.Id = user.LocationId;

        if (user.Division != null)
        {
            inserter = result.Next;
            var div = new GetUserStructureResult
            {
                Name = user.Division.Name, Id = user.DivisionId, StructureEnum = StructureEnum.Division
            };
            inserter.Add(div);
        }

        if (user.Department != null)
        {
            inserter = inserter.FirstOrDefault() != null ? inserter.FirstOrDefault()!.Next : result.Next;
            var dep = new GetUserStructureResult
            {
                Name = user.Department.Name, Id = user.DepartmentId, StructureEnum = StructureEnum.Department
            };
            inserter.Add(dep);
        }

        if (user.Group != null)
        {
            inserter = inserter.FirstOrDefault() != null ? inserter.FirstOrDefault()!.Next : result.Next;
            var group = new GetUserStructureResult
            {
                Name = user.Group.Name, Id = user.GroupId, StructureEnum = StructureEnum.Group
            };
            inserter.Add(group);
        }

        var structure = inserter.FirstOrDefault();
        if (structure == null)
        {
            return result;
        }

        switch (structure.StructureEnum)
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
                structure.Employers = employerDiv.Where(x => x.Key)
                    .Select(x => x.ToList())
                    .FirstOrDefault() ?? new List<ShortUser>();
                structure.Employees = employerDiv.Where(x => !x.Key)
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
                structure.Employers = employerDep.Where(x => x.Key)
                    .Select(x => x.ToList())
                    .FirstOrDefault() ?? new List<ShortUser>();
                structure.Employees = employerDep.Where(x => !x.Key)
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
                structure.Employers = employerGroup.Where(x => x.Key)
                    .Select(x => x.ToList())
                    .FirstOrDefault() ?? new List<ShortUser>();
                structure.Employees = employerGroup.Where(x => !x.Key)
                    .Select(x => x.ToList())
                    .FirstOrDefault() ?? new List<ShortUser>();
                break;
        }

        return result;
    }
}
