namespace Lingonberry.Api.UseCases.Employee.GetStructureFilters;

public class GetStructureFiltersQueryResult
{
    public Content Result { get; set; }
}

public class Content
{
    public string Name { get; set; }

    public List<Content> Next { get; set; }
}
