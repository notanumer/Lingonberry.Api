using Lingonberry.Api.Domain.Locations;

namespace Lingonberry.Api.UseCases.Employee.GetEmployeesByCity;

/// <summary>
/// Get employees by city result.
/// </summary>
public class GetEmployeesByCityResult
{
    /// <summary>
    /// Result.
    /// </summary>
    public List<LinkedList<BaseDomain>>? Result { get; set; }
}
