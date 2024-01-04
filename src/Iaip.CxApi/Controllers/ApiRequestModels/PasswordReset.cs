using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Iaip.CxApi.Controllers.ApiRequestModels;

public record PasswordReset
{
    [UsedImplicitly]
    public string? Username { get; init; }

    [UsedImplicitly]
    public string? NewPassword { get; init; }

    [UsedImplicitly]
    public string? ResetToken { get; init; }

    [JsonIgnore]
    public SqlParameter[] SqlParameters =>
    [
        new SqlParameter("@username", Username),
        new SqlParameter("@newpassword", NewPassword),
        new SqlParameter("@resettoken", ResetToken),
    ];
}
