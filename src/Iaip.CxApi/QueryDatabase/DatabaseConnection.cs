using Iaip.CxApi.Controllers.ApiRequestModels;
using Iaip.CxApi.Controllers.ApiResponseModels;
using Iaip.CxApi.DbHelper;
using Iaip.CxApi.Settings;

namespace Iaip.CxApi.QueryDatabase;

public class DatabaseConnection
{
    private IDbHelper DbHelper { get; }

    private static readonly string[] ValidEnvironments = { "Development", "Staging", "Production" };

    private DatabaseConnection(string env, IDbHelper dbHelper)
    {
        if (!ValidEnvironments.Contains(env)) throw new ArgumentException("Invalid route", nameof(env));
        dbHelper.SetConnectionString(AppSettings.IaipConfigOptions[env].GetConnectionString);
        DbHelper = dbHelper;
    }

    // Application methods

    public static IaipStatusResult GetStatus(IDbHelper dbHelper, string env)
    {
        var db = new DatabaseConnection(env, dbHelper);
        return new IaipStatusResult
        {
            Enabled = db.DbHelper.SpGetBoolean("dbo.IaipEnabled"),
            MinimumVersion = db.DbHelper.SpGetString("dbo.IaipMinimumVersion"),
        };
    }

    public static string ValidateLogin(IDbHelper dbHelper, string env, LoginCredentials request) =>
        new DatabaseConnection(env, dbHelper).DbHelper
            .SpGetString("iaip_user.AuthenticateIaipUser", request.SqlParameters);

    public static string ValidateSession(IDbHelper dbHelper, string env, SessionCredentials request) =>
        new DatabaseConnection(env, dbHelper).DbHelper
            .SpGetString("iaip_user.ValidateSession", request.SqlParameters);

    public static string RequestUsernameReminder(IDbHelper dbHelper, string env, UsernameRequest request) =>
        new DatabaseConnection(env, dbHelper).DbHelper
            .SpGetString("iaip_user.RequestUsernameReminder", request.SqlParameters);

    public static string RequestResetUserPassword(IDbHelper dbHelper, string env, PasswordResetRequest request) =>
        new DatabaseConnection(env, dbHelper).DbHelper
            .SpGetString("iaip_user.RequestResetUserPassword", request.SqlParameters);

    public static string ResetUserPassword(IDbHelper dbHelper, string env, PasswordReset request) =>
        new DatabaseConnection(env, dbHelper).DbHelper
            .SpGetString("iaip_user.ResetUserPassword", request.SqlParameters);
}
