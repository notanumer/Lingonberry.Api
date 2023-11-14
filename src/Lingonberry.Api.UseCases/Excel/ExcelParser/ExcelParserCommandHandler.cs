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

    // private void UploadData(Dictionary<string, Division> hashDivision, Dictionary<string, Department> hashDepartment,
    //     Dictionary<string, Group> hashGroup)
    // {
    //     foreach (var department in dbContext.Set<Department>())
    //     {
    //         hashDepartment.Add(department.Name, department);
    //     }
    //     foreach (var division in dbContext.Set<Division>())
    //     {
    //         hashDivision.Add(division.Name, division);
    //     }
    //     foreach (var group in dbContext.Set<Group>())
    //     {
    //         hashGroup.Add(group.Name, group);
    //     }
    // }

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
        for (var i = 3; i < 20; i++)
        {
            var location = new Location() { Name = ws.Cell($"C{i}").GetValue<string>() };

            var division = new Division { Name = ws.Cell($"D{i}").GetValue<string>() };

            var department = new Department
            {
                Name = ws.Cell($"E{i}").GetValue<string>(),
                Division = string.IsNullOrEmpty(division.Name) ? null : division
            };

            var group = new Group
            {
                Name = ws.Cell($"F{i}").GetValue<string>(),
                Department = string.IsNullOrEmpty(department.Name) ? null : department
            };

            var user = new User
            {
                CreatedAt = DateTime.Now,
                IsVacancy = ws.Cell($"H{i}").GetValue<string>() == "Вакансия"
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
                            if (division.Locations == null)
                            {
                                division.Locations = new List<Location>() { location };
                            }
                            else
                            {
                                division.Locations.Add(location);
                            }
                            hashLocation.Add(value, location);
                            user.Location = location;
                        }
                        else
                        {
                            if (division.Locations == null)
                            {
                                division.Locations = new List<Location>() { hashLocation[value] };
                            }
                            else
                            {
                                division.Locations.Add(hashLocation[value]);
                            }
                            user.Location = hashLocation[value];
                        }
                        break;
                    case 'D':
                        if (!hashDivision.ContainsKey(value))
                        {
                            if (hashLocation[location.Name].Divisions == null)
                            {
                                hashLocation[location.Name].Divisions = new List<Division>() { division };
                            }
                            else
                            {
                                hashLocation[location.Name].Divisions.Add(division);
                            }
                            hashDivision.Add(value, division);
                            user.Division = division;
                        }
                        else
                        {
                            if (hashLocation[location.Name].Divisions == null)
                            {
                                hashLocation[location.Name].Divisions = new List<Division>() { hashDivision[value] };
                            }
                            else
                            {
                                hashLocation[location.Name].Divisions.Add(hashDivision[value]);
                            }
                            user.Division = hashDivision[value];
                        }
                        break;
                    case 'E':
                        if (!hashDepartment.ContainsKey(value))
                        {
                            hashDepartment.Add(value, department);
                            user.Department = department;
                        }
                        else
                        {
                            user.Department = hashDepartment[value];
                        }
                        break;
                    case 'F':
                        if (!hashGroup.ContainsKey(value))
                        {
                            hashGroup.Add(value, group);
                            user.Group = group;
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
