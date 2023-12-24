using Lingonberry.Api.Domain.Locations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lingonberry.Api.Infrastructure.DataAccess.ModelConfigurations;

internal class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.HasMany(d => d.Divisions)
            .WithMany(l => l.Departments);

        builder.HasMany(d => d.Groups)
            .WithMany(g => g.Departments);
    }
}
