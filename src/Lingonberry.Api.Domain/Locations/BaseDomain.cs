using Lingonberry.Api.Domain.Users;

namespace Lingonberry.Api.Domain.Locations;

public class BaseDomain
{
    public int Id { get; private set; }

    public string? Name { get; set; }

    public ICollection<User> Users { get; set; } = new List<User>();

    public ICollection<Location> Locations { get; set; } = new List<Location>();
}
