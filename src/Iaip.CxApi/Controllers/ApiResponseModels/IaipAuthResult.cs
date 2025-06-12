using Iaip.CxApi.Settings;

namespace Iaip.CxApi.Controllers.ApiResponseModels;

public record IaipAuthResult
{
    public bool Success { [UsedImplicitly] get; init; }
    public string Message { [UsedImplicitly] get; init; } = "";
    public IaipConfig? IaipConfig { [UsedImplicitly] get; init; }

    public static IaipAuthResult AuthErrorResult(string message) => new() { Message = message };

    public static IaipAuthResult AuthSuccessResult(string env, string message) =>
        new() { Success = true, Message = message, IaipConfig = AppSettings.IaipConfigOptions[env] };
}
