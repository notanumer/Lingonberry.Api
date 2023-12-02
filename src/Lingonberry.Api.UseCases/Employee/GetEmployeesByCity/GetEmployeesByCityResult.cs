﻿using Lingonberry.Api.Domain.Locations;

namespace Lingonberry.Api.UseCases.Employee.GetEmployeesByCity;

/// <summary>
/// Get employees by city result.
/// </summary>
public class GetEmployeesByCityResult
{
    /// <summary>
    /// Result.
    /// </summary>
    public List<LinkedList<List<LinkedList<List<BaseDomain>>>>>? Result { get; set; }

    /// <summary>
    /// User count.
    /// </summary>
    public int UserCount { get; set; }

    /// <summary>
    /// Vacancy count.
    /// </summary>
    public int VacancyCount { get; set; }
}
