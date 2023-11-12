using Lingonberry.Api.Domain.Users;

namespace Lingonberry.Api.Domain.Locations;

public class Group
{
    public int Id { get; private set; }

    required public string Name { get; set; }

    public ICollection<User> Users { get; set; } = new List<User>();
}
