namespace Lingonberry.Api.Domain.Locations.Helpers;

public class Structure
{
    public string Name { get; set; }

    public StructureEnum StructureEnum { get; set; }
}

public enum StructureEnum
{
    Division,

    Department,

    Group,

    User
}
