using Lingonberry.Api.Domain.Users;

namespace Lingonberry.Api.Domain.Locations;

public class Group : BaseDomain
{
    public int? DepartmentId { get; set; }

    public Department? Department { get; set; }

    public ICollection<Location> Locations { get; set; } = new List<Location>();
}
