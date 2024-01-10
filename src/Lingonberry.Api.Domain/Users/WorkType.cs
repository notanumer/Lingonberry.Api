using System.ComponentModel;

namespace Lingonberry.Api.Domain.Users;

/// <summary>
/// Work type of employee.
/// </summary>
public enum WorkType
{
    /// <summary>
    /// Бизнес.
    /// </summary>
    [Description("Бизнес")]
    Business = 1,

    /// <summary>
    /// БС3101.
    /// </summary>
    [Description("БСЗ101")]
    BSZ101 = 2,

    /// <summary>
    /// БП/Цифра.
    /// </summary>
    [Description("БП/Цифра")]
    BPDigit = 3,

    /// <summary>
    /// Сервис.
    /// </summary>
    [Description("Сервис")]
    Service = 4
}
