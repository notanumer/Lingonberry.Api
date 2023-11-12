using Lingonberry.Api.Domain.Users;

namespace Lingonberry.Api.Domain.Locations;

public class Department
{
    public int Id { get; private set; }

    required public string Name { get; set; }

    public ICollection<User> Users { get; set; } = new List<User>();

    public ICollection<Group> Groups { get; set; } = new List<Group>();
}
