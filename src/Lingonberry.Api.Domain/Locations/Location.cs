using Lingonberry.Api.Domain.Users;

namespace Lingonberry.Api.Domain.Locations;

/// <summary>
/// Location.
/// </summary>
public class Location
{
    public int Id { get; private set; }

    required public string Name { get; set; }

    public ICollection<Division> Divisions { get; set; }

    public ICollection<User> Users { get; set; } = new List<User>();

    public ICollection<Group> Groups { get; set; }

    public ICollection<Department> Departments { get; set; }
}
