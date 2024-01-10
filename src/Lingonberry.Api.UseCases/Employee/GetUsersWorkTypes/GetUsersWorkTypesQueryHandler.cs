using Lingonberry.Api.Infrastructure.Abstractions.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Saritasa.Tools.Common.Utils;

namespace Lingonberry.Api.UseCases.Employee.GetUsersWorkTypes;

/// <summary>
/// Handler for <see cref="GetUsersWorkTypesQuery"/>.
/// </summary>
internal class GetUsersWorkTypesQueryHandler(IAppDbContext appDbContext) : IRequestHandler<GetUsersWorkTypesQuery, ICollection<string>>
{
    private readonly IAppDbContext appDbContext = appDbContext;

    /// <inheritdoc/>
    public async Task<ICollection<string>> Handle(GetUsersWorkTypesQuery request, CancellationToken cancellationToken)
    {
        var users = appDbContext.Users.AsNoTracking();

        if (!string.IsNullOrEmpty(request.LocationName))
        {
            users = users.Where(u => u.Location!.Name == request.LocationName);
        }
        if (!string.IsNullOrEmpty(request.DivisionName))
        {
            users = users.Where(u => u.Division!.Name == request.DivisionName);
        }
        if (!string.IsNullOrEmpty(request.DepartmentName))
        {
            users = users.Where(u => u.Department!.Name == request.DepartmentName);
        }
        if (!string.IsNullOrEmpty(request.GroupName))
        {
            users = users.Where(u => u.Group!.Name == request.GroupName);
        }
        if (!string.IsNullOrEmpty(request.PositionName))
        {
            users = users.Where(u => u.UserPositionName == request.PositionName);
        }

        var userPositions = await users.Select(u => u.WorkType).Where(w => w != 0).Distinct().ToListAsync(cancellationToken);

        return userPositions.Select(u => EnumUtils.GetDescription(u)).ToList();
    }
}
