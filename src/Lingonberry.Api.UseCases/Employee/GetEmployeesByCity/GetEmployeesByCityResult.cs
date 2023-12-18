﻿using Lingonberry.Api.Domain.Locations;
using Lingonberry.Api.Domain.Locations.Helpers;
using Lingonberry.Api.UseCases.Employee.Common;

namespace Lingonberry.Api.UseCases.Employee.GetEmployeesByCity;

/// <summary>
/// Get employees by city result.
/// </summary>
public class GetEmployeesByCityResult
{
    /// <summary>
    /// Id.
    /// </summary>
    public int? Id { get; set; }

    /// <summary>
    /// Structure name.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Structure type.
    /// </summary>
    public StructureEnum? StructureEnum { get; set; }

    /// <summary>
    /// User count.
    /// </summary>
    public int UserCount { get; set; }

    /// <summary>
    /// Vacancy count.
    /// </summary>
    public int VacancyCount { get; set; }

    /// <summary>
    /// Next content.
    /// </summary>
    public List<GetEmployeesByCityResult> Next { get; set; } = new();

    /// <summary>
    /// Employer.
    /// </summary>
    public ICollection<ShortUser>? Employers { get; set; }

    /// <summary>
    /// Employees.
    /// </summary>
    public ICollection<ShortUser>? Employees { get; set; }
}
