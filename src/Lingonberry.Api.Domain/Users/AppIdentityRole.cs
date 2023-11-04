using Microsoft.AspNetCore.Identity;

namespace Lingonberry.Api.Domain.Users;

/// <summary>
/// Custom application identity role.
/// </summary>
public class AppIdentityRole : IdentityRole<int>
{
}
