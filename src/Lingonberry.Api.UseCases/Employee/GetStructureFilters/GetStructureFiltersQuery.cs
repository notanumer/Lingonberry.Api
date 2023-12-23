using MediatR;

namespace Lingonberry.Api.UseCases.Employee.GetStructureFilters;

/// <summary>
/// Get structure filters query.
/// </summary>
public class GetStructureFiltersQuery : IRequest<GetStructureFiltersQueryResult>
{
    /// <summary>
    /// Name of location.
    /// </summary>
    required public string LocationName { get; init; }
}
