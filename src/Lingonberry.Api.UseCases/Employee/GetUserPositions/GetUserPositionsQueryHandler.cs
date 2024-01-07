using Lingonberry.Api.Infrastructure.Abstractions.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Lingonberry.Api.UseCases.Employee.GetUserPositions;

/// <summary>
/// Handler for <see cref="GetUserPositionsQuery"/>.
/// </summary>
internal class GetUserPositionsQueryHandler(IAppDbContext appDbContext) : IRequestHandler<GetUserPositionsQuery, ICollection<string?>>
{
    private readonly IAppDbContext appDbContext = appDbContext;

    /// <inheritdoc/>
    public async Task<ICollection<string?>> Handle(GetUserPositionsQuery request, CancellationToken cancellationToken)
    {
        var users = appDbContext.Users.Where(u => u.Location!.Name == request.LocationName);
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
        return await users.Select(u => u.Position).Distinct().ToListAsync(cancellationToken);
    }
}
