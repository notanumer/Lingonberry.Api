using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Lingonberry.Api.Infrastructure.Abstractions.Interfaces;
using Lingonberry.Api.Infrastructure.DataAccess;
using Lingonberry.Api.UseCases.Users.AuthenticateUser;
using Lingonberry.Api.Web.Infrastructure.Jwt;
using Lingonberry.Api.Web.Infrastructure.Web;

namespace Lingonberry.Api.Web.Infrastructure.DependencyInjection;

/// <summary>
/// System specific dependencies.
/// </summary>
internal static class SystemModule
{
    /// <summary>
    /// Register dependencies.
    /// </summary>
    /// <param name="services">Services.</param>
    public static void Register(IServiceCollection services)
    {
        services.AddSingleton<IJsonHelper, SystemTextJsonHelper>();
        services.AddScoped<IAuthenticationTokenService, SystemJwtTokenService>();
        services.AddScoped<IAppDbContext>(s => s.GetRequiredService<AppDbContext>());
        services.AddScoped<ILoggedUserAccessor, LoggedUserAccessor>();
    }
}
