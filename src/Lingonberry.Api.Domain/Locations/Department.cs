using Lingonberry.Api.Domain.Users;

namespace Lingonberry.Api.Domain.Locations;

public class Department : BaseDomain
{
    public ICollection<Group> Groups { get; set; } = new List<Group>();

    public int? DivisionId { get; set; }

    public ICollection<Division> Divisions { get; set; } = new List<Division>();
}
