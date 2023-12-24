using Lingonberry.Api.Domain.Locations.Helpers;

namespace Lingonberry.Api.UseCases.Structure.GetLocationData.Dto;

/// <summary>
/// Get Structure by location result.
/// </summary>
public class GetLocationDataDto
{
    /// <summary>
    /// Structure name.
    /// </summary>
    required public string Name { get; set; }

    /// <summary>
    /// Structure id.
    /// </summary>
    required public int Id { get; set; }

    /// <summary>
    /// Structure enum.
    /// </summary>
    required public StructureEnum StructureEnum { get; set; }
}
