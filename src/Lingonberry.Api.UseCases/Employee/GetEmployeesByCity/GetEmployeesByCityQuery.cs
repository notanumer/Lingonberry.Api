using System.ComponentModel.DataAnnotations;
using Lingonberry.Api.Domain.Locations.Helpers;
using MediatR;

namespace Lingonberry.Api.UseCases.Employee.GetEmployeesByCity;

/// <summary>
/// Get employees by city query.
/// </summary>
public class GetEmployeesByCityQuery : IRequest<GetEmployeesByCityResult>
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
    /// City name.
    /// </summary>
    required public string CityName { get; init; }

    /// <summary>
    /// Filtering by salary.
    /// </summary>
    public SalaryFilterOrder? SalaryFilterOrder { get; init; }

    /// <summary>
    /// Structure.
    /// </summary>
    //public Structure Structure { get; set; }
}
