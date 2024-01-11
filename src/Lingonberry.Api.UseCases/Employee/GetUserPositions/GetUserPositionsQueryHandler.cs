using Lingonberry.Api.Infrastructure.Abstractions.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Lingonberry.Api.UseCases.Employee.GetUserPositions;

/// <summary>
/// Handler for <see cref="GetUserPositionsQuery"/>.
/// </summary>
internal class GetUserPositionsQueryHandler : IRequestHandler<GetUserPositionsQuery, ICollection<string?>>
{
    private readonly IAppDbContext appDbContext;

    public GetUserPositionsQueryHandler(IAppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    /// <inheritdoc/>
    public async Task<ICollection<string?>> Handle(GetUserPositionsQuery request, CancellationToken cancellationToken)
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
        var userPositions = await users.Select(u => u.Position).Distinct().ToListAsync(cancellationToken);
        userPositions.RemoveAll(string.IsNullOrEmpty);
        return userPositions;
    }
}
