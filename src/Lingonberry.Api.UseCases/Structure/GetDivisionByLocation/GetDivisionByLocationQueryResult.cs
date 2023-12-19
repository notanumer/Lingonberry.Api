namespace Lingonberry.Api.UseCases.Structure.GetDivisionByLocation;

/// <summary>
/// Get division by location result.
/// </summary>
public class GetDivisionByLocationQueryResult
{
    /// <summary>
    /// Division name.
    /// </summary>
    required public string Name { get; set; }

    /// <summary>
    /// Division id.
    /// </summary>
    required public int Id { get; set; }
}
