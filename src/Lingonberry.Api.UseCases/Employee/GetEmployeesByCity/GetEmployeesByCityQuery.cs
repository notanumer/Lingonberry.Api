using System.ComponentModel;
using Lingonberry.Api.UseCases.Handlers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lingonberry.Api.UseCases.Employee.GetEmployeesByCity;

/// <summary>
/// Get employees by city query.
/// </summary>
public class GetEmployeesByCityQuery : IRequest<GetEmployeesByCityResult>
{
    /// <summary>
    /// Location name.
    /// </summary>
    [DefaultValue("Брусника.Екатеринбург")]
    required public string LocationName { get; init; }

    /// <summary>
    /// List with filter displays.
    /// </summary>
    [ModelBinder(typeof(SwaggerCollection<List<FilterDisplays>>))]
    required public List<FilterDisplays> FilterDisplaysList { get; init; }
}
