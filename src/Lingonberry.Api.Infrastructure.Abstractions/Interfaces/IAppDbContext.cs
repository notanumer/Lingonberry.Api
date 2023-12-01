using Lingonberry.Api.Domain.Locations;
using Lingonberry.Api.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Lingonberry.Api.Infrastructure.Abstractions.Interfaces;

/// <summary>
/// Application abstraction for unit of work.
/// </summary>
public interface IAppDbContext : IDbContextWithSets, IDisposable
{
    /// <summary>
    /// Users.
    /// </summary>
    DbSet<User> Users { get; }

    DbSet<Division> Divisions { get; }

    DbSet<Location> Locations { get; }

    DbSet<Department> Departments { get; }

    DbSet<Group> Groups { get; }
}
