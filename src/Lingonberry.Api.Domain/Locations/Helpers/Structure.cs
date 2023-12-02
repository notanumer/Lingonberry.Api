namespace Lingonberry.Api.Domain.Locations.Helpers;

public class Structure
{
    required public bool IsDivision { get; init; } = true;

    required public bool IsDepartment { get; init; } = true;

    required public bool IsGroup { get; init; } = true;

    required public bool IsUser { get; init; } = true;
}
