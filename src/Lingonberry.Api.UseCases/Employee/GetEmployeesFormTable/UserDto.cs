using Lingonberry.Api.Domain.Users;

namespace Lingonberry.Api.UseCases.Employee.GetEmployeesFormTable;

public class UserDto
{
    required public string FullName { get; init; }

    required public string NumberPosition { get; init; }

    required public string LegalEntity { get; init; }

    public string? Location { get; init; }

    public string? Division { get; init; }

    public string? Department { get; init; }

    public string? Group { get; init; }

    required public string Position { get; init; }

    required public WorkType WorkType { get; init; }
}
