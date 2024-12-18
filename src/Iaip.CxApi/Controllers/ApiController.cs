using Iaip.CxApi.Controllers.ApiRequestModels;
using Iaip.CxApi.Controllers.ApiResponseModels;
using Iaip.CxApi.DbHelper;
using Iaip.CxApi.QueryDatabase;
using Microsoft.AspNetCore.Mvc;

namespace Iaip.CxApi.Controllers;

[ApiController]
[Route("api/{env}")]
public class ApiController(IDbHelper dbHelper, ILogger<ApiController> logger) : ControllerBase
{
    // Endpoints

    [HttpGet("status"), Produces("application/json")]
    public ActionResult<IaipStatusResult> GetStatus([FromRoute] string env) =>
        DatabaseConnection.GetStatus(dbHelper, env);

    [HttpPost("login"), Produces("application/json")]
    public ActionResult<IaipAuthResult> PostLogin([FromRoute] string env, [FromBody] LoginCredentials request)
    {
        Guard.NotNull(request);

        if (string.IsNullOrEmpty(request.Username))
            return InvalidCredentials("InvalidUsername", env, "[empty username]");

        if (string.IsNullOrEmpty(request.Password))
            return InvalidCredentials("InvalidLogin", env, $"{request.Username} [with empty password]");

        var validationResult = DatabaseConnection.ValidateLogin(dbHelper, env, request);
        return validationResult == "Success"
            ? ValidCredentials(validationResult, env, request.Username)
            : InvalidCredentials(validationResult, env, request.Username);
    }

    [HttpPost("session"), Produces("application/json")]
    public ActionResult<IaipAuthResult> PostSession([FromRoute] string env, [FromBody] SessionCredentials request)
    {
        Guard.NotNull(request);
        Guard.Positive(request.UserId);

        if (string.IsNullOrEmpty(request.Token) || string.IsNullOrEmpty(request.MachineName) ||
            string.IsNullOrEmpty(request.WindowsDomainName) || string.IsNullOrEmpty(request.WindowsUserName))
        {
            logger.LogWarning("Missing session details on {Env} for User ID {User}", env, request.UserId.ToString());
            return IaipAuthResult.AuthErrorResult(string.Empty);
        }

        var newToken = DatabaseConnection.ValidateSession(dbHelper, env, request);
        return string.IsNullOrEmpty(newToken)
            ? InvalidSession(env, request.UserId)
            : ValidSession(newToken, env, request.UserId);
    }

    [HttpPost("request-username"), Produces("text/plain")]
    public ActionResult<string> PostRequestUsernameReminder([FromRoute] string env, [FromBody] UsernameRequest request)
    {
        Guard.NotNull(request);
        Guard.NotNullOrWhiteSpace(request.Email);

        var result = DatabaseConnection.RequestUsernameReminder(dbHelper, env, request);
        logger.LogInformation("Username requested for {Email} with result {Result}", request.Email, result);
        return result;
    }

    [HttpPost("request-password-reset")]
    public ActionResult<string> PostRequestUserPasswordReset(
        [FromRoute] string env,
        [FromBody] PasswordResetRequest request)
    {
        Guard.NotNull(request);
        Guard.NotNullOrWhiteSpace(request.Username);

        var result = DatabaseConnection.RequestResetUserPassword(dbHelper, env, request);
        logger.LogInformation("Password reset request submitted for {User} with result {Result}", request.Username, result);
        return result;
    }

    [HttpPost("reset-password"), Produces("text/plain")]
    public ActionResult<string> PostResetUserPassword([FromRoute] string env, [FromBody] PasswordReset request)
    {
        Guard.NotNull(request);
        Guard.NotNullOrWhiteSpace(request.Username);
        Guard.NotNullOrWhiteSpace(request.NewPassword);
        Guard.NotNullOrWhiteSpace(request.ResetToken);

        var result = DatabaseConnection.ResetUserPassword(dbHelper, env, request);
        logger.LogInformation("Password reset submitted for {User} with result {Result}", request.Username, result);
        return result;
    }

    // Authentication result helpers

    private ActionResult<IaipAuthResult> ValidCredentials(string message, string env, string user)
    {
        logger.LogInformation("Valid credentials accepted on {Env} for {User}", env, user);
        return IaipAuthResult.AuthSuccessResult(env, message);
    }

    private ActionResult<IaipAuthResult> InvalidCredentials(string result, string env, string user)
    {
        logger.LogWarning("Invalid login credentials on {Env} for {User} with result {Result}", env, user, result);
        return IaipAuthResult.AuthErrorResult(result);
    }

    private ActionResult<IaipAuthResult> ValidSession(string message, string env, int userId)
    {
        logger.LogInformation("Valid session accepted on {Env} for User ID {User}", env, userId.ToString());
        return IaipAuthResult.AuthSuccessResult(env, message);
    }

    private ActionResult<IaipAuthResult> InvalidSession(string env, int userId)
    {
        logger.LogWarning("Invalid session token on {Env} for User ID {User}", env, userId.ToString());
        return IaipAuthResult.AuthErrorResult(string.Empty);
    }
}
