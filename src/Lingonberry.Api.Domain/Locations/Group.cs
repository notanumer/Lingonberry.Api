using Lingonberry.Api.Domain.Users;

namespace Lingonberry.Api.Domain.Locations;

public class Group : BaseDomain
{
    public ICollection<Department>? Departments { get; set; }

    public ICollection<Division>? Divisions { get; set; }
}
