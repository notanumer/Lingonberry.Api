using Lingonberry.Api.Domain.Users;

namespace Lingonberry.Api.Domain.Locations;

public class Division : BaseDomain
{
    public ICollection<Department>? Departments { get; set; }

    public ICollection<Group>? Groups { get; set; }
}
