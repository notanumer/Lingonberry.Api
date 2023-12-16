namespace Lingonberry.Api.UseCases.Structure.Common;

/// <summary>
/// Location dto.
/// </summary>
public class LocationDto
{
    /// <summary>
    /// Location name.
    /// </summary>
    required public string Name { get; set; }

    /// <summary>
    /// Location id.
    /// </summary>
    required public int Id { get; set; }
}
