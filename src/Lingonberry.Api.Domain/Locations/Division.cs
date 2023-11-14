using Lingonberry.Api.Domain.Users;

namespace Lingonberry.Api.Domain.Locations;

public class Division
{
    public int Id { get; private set; }

    required public string Name { get; set; }

    public ICollection<Department>? Departments { get; set; }

    public ICollection<User> Users { get; set; } = new List<User>();

    public ICollection<Location> Locations { get; set; } = new List<Location>();
}
