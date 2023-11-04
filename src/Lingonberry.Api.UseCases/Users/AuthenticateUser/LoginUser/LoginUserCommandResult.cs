namespace Lingonberry.Api.UseCases.Users.AuthenticateUser.LoginUser;

/// <summary>
/// Represents user login attempt to system.
/// </summary>
public class LoginUserCommandResult
{
    /// <summary>
    /// Logged user id (if success).
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// New refresh token.
    /// </summary>
    required public TokenModel TokenModel { get; set; }
}
