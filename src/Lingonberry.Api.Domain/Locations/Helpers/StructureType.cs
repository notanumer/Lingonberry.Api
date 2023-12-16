namespace Lingonberry.Api.Domain.Locations.Helpers;

public class StructureType
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
