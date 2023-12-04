using MediatR;

namespace Lingonberry.Api.UseCases.Employee.GetEmployeesFormTable;

public class GetEmployeesFormTableQueryHandler : IRequestHandler<GetEmployeesFormTableQuery, GetEmployeesFormTableResult>
{
    public Task<GetEmployeesFormTableResult> Handle(GetEmployeesFormTableQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
