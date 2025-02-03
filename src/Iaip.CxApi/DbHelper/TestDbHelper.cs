using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Iaip.CxApi.DbHelper;

public class TestDbHelper(IConfiguration configuration) : IDbHelper
{
    private const string SuccessString = "Success";
    private bool Successful { get; } = configuration.GetValue<bool>("TestDbHelperSuccessful");
    private bool Enabled { get; } = configuration.GetValue<bool>("TestDbHelperEnabled");

    public void SetConnectionString(string value) { }

    public Task<string> SpGetStringAsync(string spName) => SpGetStringAsync(spName, []);

    public Task<string> SpGetStringAsync(string spName, SqlParameter[] parameterArray) => Task.FromResult(spName switch
    {
        "iaip_user.AuthenticateIaipUser" => Successful ? SuccessString : "InvalidLogin",
        "iaip_user.ValidateSession" => Successful ? Guid.NewGuid().ToString() : string.Empty,
        "iaip_user.RequestUsernameReminder" => Successful ? SuccessString : "EmailNotExist",
        "iaip_user.RequestResetUserPassword" => Successful ? SuccessString : "InvalidUsername",
        "iaip_user.ResetUserPassword" => Successful ? SuccessString : "InvalidToken",
        "dbo.IaipMinimumVersion" => "1.2.3",
        _ => throw new ArgumentOutOfRangeException(spName),
    });

    public Task<bool> SpGetBooleanAsync(string spName) => Task.FromResult(Enabled);
}
