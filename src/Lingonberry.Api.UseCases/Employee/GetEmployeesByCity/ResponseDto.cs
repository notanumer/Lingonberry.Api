using Lingonberry.Api.Domain.Locations;

namespace Lingonberry.Api.UseCases.Employee.GetEmployeesByCity;

public class ResponseDto
{
    public LinkedList<List<Content>> Response { get; set; } = new();
}

public class Content
{
    /// <summary>
    /// Head.
    /// </summary>
    public BaseDomain Head { get; set; }

    /// <summary>
    /// Body.
    /// </summary>
    public List<BaseDomain> Body { get; set; } = new();

    /// <summary>
    /// User count.
    /// </summary>
    public int UserCount { get; set; }

    /// <summary>
    /// Vacancy count.
    /// </summary>
    public int VacancyCount { get; set; }
}
