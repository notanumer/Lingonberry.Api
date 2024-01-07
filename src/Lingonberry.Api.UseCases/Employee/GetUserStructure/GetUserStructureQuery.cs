using MediatR;

namespace Lingonberry.Api.UseCases.Employee.GetUserStructure;

/// <summary>
/// Query to get user structure selected by user id.
/// </summary>
public record GetUserStructureQuery : IRequest<GetUserStructureResult>
{
    /// <summary>
    /// User id.
    /// </summary>
    required public int UserId { get; init; }
}
