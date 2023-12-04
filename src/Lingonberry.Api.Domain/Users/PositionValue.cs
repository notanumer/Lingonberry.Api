using System.ComponentModel.DataAnnotations;

namespace Lingonberry.Api.Domain.Users;

/// <summary>
/// Значимость должности сотрудника.
/// Чем выше должность - тем выше значение.
/// </summary>
public enum PositionValue
{
    /// <summary>
    /// Специалист.
    /// </summary>
    [Display(Name = "Специалист")]
    Specialist = 1,

    /// <summary>
    /// Ведущий.
    /// </summary>
    [Display(Name = "Ведущий")]
    Leading,

    /// <summary>
    /// Главный.
    /// </summary>
    [Display(Name = "Главный")]
    Chief,

    /// <summary>
    /// Руководитель.
    /// </summary>
    [Display(Name = "Руководитель")]
    Head,

    /// <summary>
    /// Директор.
    /// </summary>
    [Display(Name = "Директор")]
    Director
}
