using Lingonberry.Api.Domain.Locations.Helpers;

namespace Lingonberry.Api.UseCases.Employee.GetEmployeesByCity;

/// <summary>
/// Filter displays.
/// </summary>
public class FilterDisplays
{
    /// <summary>
    /// Structure name.
    /// </summary>
    required public string StructureName { get; init; }

    /// <summary>
    /// Structure type.
    /// </summary>
    required public StructureEnum StructureEnum { get; init; }
}
