namespace Lingonberry.Api.Domain.Locations;

/// <summary>
/// Location.
/// </summary>
public class Location
{
    public int Id { get; private set; }

    required public string Name { get; set; }

    public ICollection<Division> Divisions { get; set; } = new List<Division>();
}
