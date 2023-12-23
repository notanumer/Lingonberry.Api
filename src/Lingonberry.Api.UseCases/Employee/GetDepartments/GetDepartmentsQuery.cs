using MediatR;

namespace Lingonberry.Api.UseCases.Employee.GetDepartments;

public record GetDepartmentsQuery : IRequest<GetDepartmentsQueryResult>
{
    required public string LocationName { get; init; }

    public ICollection<string> DivisonNames { get; init; } = new List<string>();
}
