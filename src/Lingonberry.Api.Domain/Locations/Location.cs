using Lingonberry.Api.Domain.Users;

namespace Lingonberry.Api.Domain.Locations;

/// <summary>
/// Location.
/// </summary>
public class Location
{
    public int Id { get; private set; }

    required public string Name { get; set; }

    public ICollection<Division> Divisions { get; set; } = new List<Division>();

    public ICollection<User> Users { get; set; } = new List<User>();

    public ICollection<Group> Groups { get; set; } = new List<Group>();

    public ICollection<Department> Departments { get; set; } = new List<Department>();
}
