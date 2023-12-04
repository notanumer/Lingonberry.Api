namespace Lingonberry.Api.UseCases.Employee.GetEmployeesFormTable;

public class GetEmployeesFormTableResult
{
    required public List<UserDto> Users { get; set; } = new();
}
