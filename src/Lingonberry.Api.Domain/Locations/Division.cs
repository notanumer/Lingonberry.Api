using Lingonberry.Api.Domain.Users;

namespace Lingonberry.Api.Domain.Locations;

public class Division : BaseDomain
{
    public ICollection<User> Users { get; set; } = new List<User>();

    public ICollection<Location> Locations { get; set; } = new List<Location>();
}
