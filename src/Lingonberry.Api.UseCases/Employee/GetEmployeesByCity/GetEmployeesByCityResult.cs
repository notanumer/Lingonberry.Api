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
    public LinkedListNode<BaseDomain> Result { get; set; }
}