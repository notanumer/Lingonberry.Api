using System.ComponentModel.DataAnnotations;

namespace Lingonberry.Api.Domain.Users;

public enum WorkType
{
    [Display(Name = "Бизнес")]
    Business = 1,

    [Display(Name = "БСЗ101")]
    BSZ101 = 2,

    [Display(Name = "БП/Цифра")]
    BPDigit = 3,

    [Display(Name = "Сервис")]
    Service = 4
}
