using MediatR;

namespace Lingonberry.Api.UseCases.Employee.GetLocationsNames;

/// <summary>
/// Query to get locations names.
/// </summary>
public record GetLocationsNamesQuery() : IRequest<ICollection<string>>;
