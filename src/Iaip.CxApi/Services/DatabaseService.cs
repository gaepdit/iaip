using Iaip.CxApi.Controllers.ApiRequestModels;
using Iaip.CxApi.Controllers.ApiResponseModels;
using Iaip.CxApi.DbHelper;
using Iaip.CxApi.Settings;

namespace Iaip.CxApi.Services;

public class DatabaseService
{
    private IDbHelper DbHelper { get; }

    private static readonly string[] ValidEnvironments = ["Development", "Staging", "Production"];

    private DatabaseService(string env, IDbHelper dbHelper)
    {
        if (!ValidEnvironments.Contains(env)) throw new ArgumentException("Invalid route", nameof(env));
        dbHelper.SetConnectionString(AppSettings.IaipConfigOptions[env].GetConnectionString);
        DbHelper = dbHelper;
    }

    // Application methods

    public static async Task<IaipStatusResult> GetStatus(IDbHelper dbHelper, string env)
    {
        var db = new DatabaseService(env, dbHelper);
        var spIaipEnabled = db.DbHelper.SpGetBooleanAsync("dbo.IaipEnabled");
        var spMinimumVersion = db.DbHelper.SpGetStringAsync("dbo.IaipMinimumVersion");
        return new IaipStatusResult { Enabled = await spIaipEnabled, MinimumVersion = await spMinimumVersion };
    }

    public static Task<string> ValidateLogin(IDbHelper dbHelper, string env, LoginCredentials request) =>
        new DatabaseService(env, dbHelper).DbHelper
            .SpGetStringAsync("iaip_user.AuthenticateIaipUser", request.SqlParameters);

    public static Task<string> ValidateSession(IDbHelper dbHelper, string env, SessionCredentials request) =>
        new DatabaseService(env, dbHelper).DbHelper
            .SpGetStringAsync("iaip_user.ValidateSession", request.SqlParameters);

    public static Task<string> RequestUsernameReminder(IDbHelper dbHelper, string env, UsernameRequest request) =>
        new DatabaseService(env, dbHelper).DbHelper
            .SpGetStringAsync("iaip_user.RequestUsernameReminder", request.SqlParameters);

    public static Task<string> RequestResetUserPassword(IDbHelper dbHelper, string env, PasswordResetRequest request) =>
        new DatabaseService(env, dbHelper).DbHelper
            .SpGetStringAsync("iaip_user.RequestResetUserPassword", request.SqlParameters);

    public static Task<string> ResetUserPassword(IDbHelper dbHelper, string env, PasswordReset request) =>
        new DatabaseService(env, dbHelper).DbHelper
            .SpGetStringAsync("iaip_user.ResetUserPassword", request.SqlParameters);
}
