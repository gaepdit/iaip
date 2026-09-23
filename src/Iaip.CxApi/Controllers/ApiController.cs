using Iaip.CxApi.Controllers.ApiRequestModels;
using Iaip.CxApi.Controllers.ApiResponseModels;
using Iaip.CxApi.DbHelper;
using Iaip.CxApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ZLogger;

namespace Iaip.CxApi.Controllers;

[ApiController]
[Route("api/{env}")]
public class ApiController(IDbHelper dbHelper, ILogger<ApiController> logger) : ControllerBase
{
    // Endpoints

    [HttpGet("status"), Produces("application/json")]
    public Task<IaipStatusResult> GetStatus([FromRoute] string env) =>
        DatabaseService.GetStatus(dbHelper, env);

    [HttpPost("login"), Produces("application/json")]
    public async Task<ActionResult<IaipAuthResult>> PostLogin([FromRoute] string env,
        [FromBody] LoginCredentials request)
    {
        Guard.NotNull(request);

        if (string.IsNullOrEmpty(request.Username))
            return InvalidCredentials("InvalidUsername", env, "[empty username]");

        if (string.IsNullOrEmpty(request.Password))
            return InvalidCredentials("InvalidLogin", env, $"{request.Username} [with empty password]");

        var validationResult = await DatabaseService.ValidateLogin(dbHelper, env, request);
        return validationResult == "Success"
            ? ValidCredentials(validationResult, env, request.Username)
            : InvalidCredentials(validationResult, env, request.Username);
    }

    [HttpPost("session"), Produces("application/json")]
    public async Task<ActionResult<IaipAuthResult>> PostSession([FromRoute] string env,
        [FromBody] SessionCredentials request)
    {
        Guard.NotNull(request);
        Guard.Positive(request.UserId);

        if (string.IsNullOrEmpty(request.Token) || string.IsNullOrEmpty(request.MachineName) ||
            string.IsNullOrEmpty(request.WindowsDomainName) || string.IsNullOrEmpty(request.WindowsUserName))
        {
            logger.ZLogWarning($"Missing session details on {env} for User ID {request.UserId:@User}");
            return IaipAuthResult.AuthErrorResult(string.Empty);
        }

        var newToken = await DatabaseService.ValidateSession(dbHelper, env, request);
        return string.IsNullOrEmpty(newToken)
            ? InvalidSession(env, request.UserId)
            : ValidSession(newToken, env, request.UserId);
    }

    [HttpPost("request-username"), Produces("text/plain")]
    public async Task<string> PostRequestUsernameReminder([FromRoute] string env, [FromBody] UsernameRequest request)
    {
        Guard.NotNull(request);
        Guard.NotNullOrWhiteSpace(request.Email);

        var result = await DatabaseService.RequestUsernameReminder(dbHelper, env, request);
        logger.ZLogInformation($"Username requested for {request.Email} with result {result}");
        return result;
    }

    [HttpPost("request-password-reset"), Produces("text/plain")]
    public async Task<string> PostRequestUserPasswordReset(
        [FromRoute] string env,
        [FromBody] PasswordResetRequest request)
    {
        Guard.NotNull(request);
        Guard.NotNullOrWhiteSpace(request.Username);

        var result = await DatabaseService.RequestResetUserPassword(dbHelper, env, request);
        logger.ZLogInformation($"Password reset request submitted for {request.Username:@User} with result {result}");
        return result;
    }

    [HttpPost("reset-password"), Produces("text/plain")]
    public async Task<string> PostResetUserPassword([FromRoute] string env, [FromBody] PasswordReset request)
    {
        Guard.NotNull(request);
        Guard.NotNullOrWhiteSpace(request.Username);
        Guard.NotNullOrWhiteSpace(request.NewPassword);
        Guard.NotNullOrWhiteSpace(request.ResetToken);

        var result = await DatabaseService.ResetUserPassword(dbHelper, env, request);
        logger.ZLogInformation($"Password reset submitted for {request.Username:@User} with result {result}");
        return result;
    }

    // Authentication result helpers

    private IaipAuthResult ValidCredentials(string message, string env, string user)
    {
        logger.ZLogInformation($"Valid credentials accepted on {env} for {user}");
        return IaipAuthResult.AuthSuccessResult(env, message);
    }

    private IaipAuthResult InvalidCredentials(string result, string env, string user)
    {
        logger.ZLogWarning($"Invalid login credentials on {env} for {user} with result {result}");
        return IaipAuthResult.AuthErrorResult(result);
    }

    private IaipAuthResult ValidSession(string message, string env, int userId)
    {
        logger.ZLogInformation($"Valid session accepted on {env} for User ID {userId:@User}");
        return IaipAuthResult.AuthSuccessResult(env, message);
    }

    private IaipAuthResult InvalidSession(string env, int userId)
    {
        logger.ZLogWarning($"Invalid session token on {env} for User ID {userId:@User}");
        return IaipAuthResult.AuthErrorResult(string.Empty);
    }
}
