using ClosedXML.Excel;
using Lingonberry.Api.Domain.Locations;
using Lingonberry.Api.Domain.Users;
using Lingonberry.Api.Infrastructure.Abstractions.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Group = Lingonberry.Api.Domain.Locations.Group;

namespace Lingonberry.Api.UseCases.Excel.ExcelParser;

/// <summary>
/// Excel parser.
/// </summary>
public class ExcelParserCommandHandler : IRequestHandler<ExcelParserCommand>
{
    private readonly ILogger<ExcelParserCommandHandler> logger;
    private readonly IAppDbContext dbContext;

    /// <summary>
    /// Constructor.
    /// </summary>
    public ExcelParserCommandHandler(ILogger<ExcelParserCommandHandler> logger, IAppDbContext dbContext)
    {
        this.logger = logger;
        this.dbContext = dbContext;
    }

    /// <inheritdoc />
    public async Task Handle(ExcelParserCommand request, CancellationToken cancellationToken)
    {
        var hashDivision = new Dictionary<string, Division>();
        var hashDepartment = new Dictionary<string, Department>();
        var hashGroup = new Dictionary<string, Group>();
        var hashLocation = new Dictionary<string, Location>();

        using var stream = new MemoryStream();
        await request.File.CopyToAsync(stream, cancellationToken);

        using var wbook = new XLWorkbook(stream);
        var ws = wbook.Worksheet(1);

        var countRow = ws.Rows().Count();
        var users = new List<User>();
        for (var i = 3; i < countRow; i++)
        {
            var user = new User
            {
                CreatedAt = DateTime.Now,
                IsVacancy = ws.Cell($"H{i}").GetValue<string>() == "Вакансия",
                Position = ws.Cell($"G{i}").GetValue<string>()
            };

            if (!user.IsVacancy)
            {
                var name = ws.Cell($"H{i}").GetValue<string>().Split(" ");
                user.FirstName = name[0];
                user.LastName = name[1];
                user.Patronymic = name[2];
            }

            foreach (var j in ws.Range($"C{i}:F{i}").Cells())
            {
                var value = j.GetValue<string>();

                if (string.IsNullOrEmpty(value))
                {
                    continue;
                }

                switch (j.Address.ToString()![0])
                {
                    case 'C':
                        if (!hashLocation.ContainsKey(value))
                        {
                            var l = new Location() { Name = value };
                            hashLocation.Add(value, l );
                            user.Location = l;
                        }
                        else
                        {
                            user.Location = hashLocation[value];
                        }
                        break;
                    case 'D':
                        if (!hashDivision.ContainsKey(value))
                        {
                            var d = new Division { Name = value };
                            user.Division = d;
                            hashDivision.Add(value, d);
                        }
                        else
                        {
                            user.Division = hashDivision[value];
                        }

                        if (user.Location!.Divisions == null)
                        {
                            user.Location!.Divisions = new List<Division> { user.Division };
                        }
                        else
                        {
                            user.Location!.Divisions.Add(user.Division);
                        }

                        user.Division.Locations.Add(user.Location);
                        break;
                    case 'E':
                        if (!hashDepartment.ContainsKey(value))
                        {
                            var d = new Department
                            {
                                Name = value,
                                Division = user.Division
                            };
                            user.Department = d;
                            if (user.Division == null)
                            {
                                if (user.Location!.Departments == null)
                                {
                                    user.Location!.Departments = new List<Department> { user.Department };
                                }
                                else
                                {
                                    user.Location!.Departments.Add(user.Department);
                                }
                                user.Department.Locations.Add(user.Location!);
                            }
                            hashDepartment.Add(value, d);
                        }
                        else
                        {
                            user.Department = hashDepartment[value];
                        }
                        break;
                    case 'F':
                        if (!hashGroup.ContainsKey(value))
                        {
                            var group = new Group
                            {
                                Name = value,
                                Department = user.Department
                            };
                            user.Group = group;
                            if (user.Division == null && user.Department == null)
                            {
                                if (user.Location!.Groups == null)
                                {
                                    user.Location!.Groups = new List<Group> { user.Group };
                                }
                                else
                                {
                                    user.Location!.Groups.Add(user.Group);
                                }
                                user.Group.Locations.Add(user.Location!);
                            }
                            hashGroup.Add(value, group);
                        }
                        else
                        {
                            user.Group = hashGroup[value];
                        }
                        break;
                }
            }

            users.Add(user);
        }

        await dbContext.Users.AddRangeAsync(users, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
