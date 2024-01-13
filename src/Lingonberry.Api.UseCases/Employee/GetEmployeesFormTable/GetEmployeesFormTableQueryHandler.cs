using Lingonberry.Api.Domain.Locations.Helpers;
using Lingonberry.Api.Domain.Users;
using Lingonberry.Api.Infrastructure.Abstractions.Interfaces;
using Lingonberry.Api.UseCases.Handlers;
using Lingonberry.Api.UseCases.Users.GetUserById;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Saritasa.Tools.Common.Pagination;
using Saritasa.Tools.Common.Utils;

namespace Lingonberry.Api.UseCases.Employee.GetEmployeesFormTable;

/// <summary>
/// Get employees form table handler.
/// </summary>
public class GetEmployeesFormTableQueryHandler : IRequestHandler<GetEmployeesFormTableQuery, GetEmployeesFormTableResult>
{
    private readonly ILogger<GetEmployeesFormTableQueryHandler> logger;
    private readonly IAppDbContext dbContext;

    /// <summary>
    /// Constructor.
    /// </summary>
    public GetEmployeesFormTableQueryHandler(ILogger<GetEmployeesFormTableQueryHandler> logger, IAppDbContext dbContext)
    {
        this.logger = logger;
        this.dbContext = dbContext;
    }

    /// <inheritdoc />
    public async Task<GetEmployeesFormTableResult> Handle(GetEmployeesFormTableQuery request, CancellationToken cancellationToken)
    {
        var users = dbContext.Users
            .Include(u => u.Location)
            .Include(u => u.Division)
            .Include(u => u.Department)
            .Include(u => u.Group)
            .OrderBy(u => u.Location!.Name)
            .Where(u => u.WorkType != 0);

        if (request.LocationName != null)
        {
            users = users.Where(u => u.Location!.Name == request.LocationName);
        }

        if (request.DivisionName != null)
        {
            users = users.Where(u => u.Division!.Name == request.DivisionName);
        }

        if (request.DepartmentName != null)
        {
            users = users.Where(u => u.Department!.Name == request.DepartmentName);
        }

        if (request.GroupName != null)
        {
            users = users.Where(u => u.Group!.Name == request.GroupName);
        }

        if (request.FullName != null)
        {
            switch (request.FullName)
            {
                case SortOrder.Ascending:
                    users = users.OrderBy(u => u.FirstName);
                    break;
                case SortOrder.Descending:
                    users = users.OrderByDescending(u => u.FirstName);
                    break;
            }
        }

        if (request.UserNumber != null)
        {
            switch (request.UserNumber)
            {
                case SortOrder.Ascending:
                    users = users.OrderBy(u => u.UserNumber);
                    break;
                case SortOrder.Descending:
                    users = users.OrderByDescending(u => u.UserNumber);
                    break;
            }
        }

        if (request.UserPosition != null)
        {
            var position = DisplayEnum.GetValueFromName<PositionValue>(request.UserPosition.Split(" ")[0]);
            users = users.Where(u => u.UserPosition == position);
        }

        if (request.WorkType != null)
        {
            var resultPair = EnumUtils
                .GetValuesWithDescriptions<WorkType>()
                .FirstOrDefault(pair => pair.Value == request.WorkType);
            var type = (WorkType)int.Parse(resultPair.Key);
            users = users.Where(u => u.WorkType == type);
        }

        var selectedUsers = await users.ToListAsync(cancellationToken);
        if (!string.IsNullOrEmpty(request.UserFullName))
        {
            selectedUsers = selectedUsers.Where(u => u.FullName.ToLower().Equals(request.UserFullName.ToLower())).ToList();
        }

        var usersPaged = PagedListFactory.FromSource(selectedUsers,
            page: request.Page, pageSize: request.PageSize);

        var result = new GetEmployeesFormTableResult() { Page = request.Page, Total = usersPaged.TotalCount };

        foreach (var user in usersPaged)
        {
            result.Items.Add(new UserDetailsDto
            {
                Id = user.Id,
                FullName = user.FullName,
                UserNumber = user.UserNumber,
                LegalEntity = "БСЗ",
                Location = user.Location != null ? user.Location.Name : "",
                Division = user.Division != null ? user.Division.Name : "",
                Department = user.Department != null ? user.Department.Name : "",
                Group = user.Group != null ? user.Group.Name : "",
                Position = user.Position,
                WorkType = EnumUtils.GetDescription(user.WorkType)
            });
        }

        return result;
    }
}
