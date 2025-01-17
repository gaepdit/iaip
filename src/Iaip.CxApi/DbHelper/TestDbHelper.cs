﻿using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Iaip.CxApi.DbHelper;

public class TestDbHelper(IConfiguration configuration) : IDbHelper
{
    private const string SuccessString = "Success";
    private bool Successful { get; } = configuration.GetValue<bool>("TestDbHelperSuccessful");
    private bool Enabled { get; } = configuration.GetValue<bool>("TestDbHelperEnabled");

    public void SetConnectionString(string value) { }

    public string SpGetString(string spName) => SpGetString(spName, []);

    public string SpGetString(string spName, SqlParameter[] parameterArray) => spName switch
    {
        "iaip_user.AuthenticateIaipUser" => Successful ? SuccessString : "InvalidLogin",
        "iaip_user.ValidateSession" => Successful ? Guid.NewGuid().ToString() : string.Empty,
        "iaip_user.RequestUsernameReminder" => Successful ? SuccessString : "EmailNotExist",
        "iaip_user.UpdateUserPassword" => Successful ? SuccessString : "InvalidLogin",
        "iaip_user.ResetUserPassword" => Successful ? SuccessString : "InvalidToken",
        "dbo.IaipMinimumVersion" => "1.2.3",
        _ => throw new ArgumentOutOfRangeException(spName),
    };

    public bool SpGetBoolean(string spName) => Enabled;
    public bool SpGetBoolean(string spName, SqlParameter[] parameterArray) => Enabled;
    public int SpRunCommand(string spName, SqlParameter[] parameterArray) => Successful ? 0 : -1;
}
