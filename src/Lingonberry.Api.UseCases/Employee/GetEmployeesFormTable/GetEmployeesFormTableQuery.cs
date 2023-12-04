using System.ComponentModel.DataAnnotations;
using Lingonberry.Api.Domain.Locations.Helpers;
using MediatR;

namespace Lingonberry.Api.UseCases.Employee.GetEmployeesFormTable;

/// <summary>
/// Get employees form table query.
/// </summary>
public class GetEmployeesFormTableQuery : IRequest<GetEmployeesFormTableResult>
{
    /// <summary>
    /// Page.
    /// </summary>
    [Required]
    public int Page { get; init; } = 1;

    /// <summary>
    /// Page size.
    /// </summary>
    [Required]
    public int PageSize { get; init; } = 20;

    /// <summary>
    /// Location name.
    /// </summary>
    public string? LocationName { get; init; }

    /// <summary>
    /// Division name.
    /// </summary>
    public string? DivisionName { get; init; }

    /// <summary>
    /// Department name.
    /// </summary>
    public string? DepartmentName { get; init; }

    /// <summary>
    /// Group name.
    /// </summary>
    public string? GroupName { get; init; }

    /// <summary>
    /// Filter full name.
    /// </summary>
    public SortOrder? FullName { get; init; }

    /// <summary>
    /// Filter user number.
    /// </summary>
    public SortOrder? UserNumber { get; init; }

    /// <summary>
    /// Filter user position.
    /// </summary>
    public SortOrder? UserPosition { get; init; }

    /// <summary>
    /// Filter work type.
    /// </summary>
    public SortOrder? WorkType { get; init; }
}
