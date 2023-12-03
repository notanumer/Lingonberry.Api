using Lingonberry.Api.Domain.Users;

namespace Lingonberry.Api.UseCases.Employee.GetEmployeesByCity;

public class Content
{
    /// <summary>
    /// Structure name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Next content.
    /// </summary>
    public List<Content> Next { get; set; } = new();

    /// <summary>
    /// User count.
    /// </summary>
    public int UserCount { get; set; }

    /// <summary>
    /// Vacancy count.
    /// </summary>
    public int VacancyCount { get; set; }

    /// <summary>
    /// Users.
    /// </summary>
    public ICollection<User>? Users { get; set; }
}
