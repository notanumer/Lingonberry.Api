using MediatR;

namespace Lingonberry.Api.UseCases.Employee.GetDivisionsNames;

/// <summary>
/// Get structure filters query.
/// </summary>
public class GetDivisionsNamesQuery : IRequest<GetDivisionsNamesQueryResult>
{
    /// <summary>
    /// Name of location.
    /// </summary>
    public string? LocationName { get; init; }
}
