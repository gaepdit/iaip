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
        new("@username", Username),
        new("@newpassword", NewPassword),
        new("@resettoken", ResetToken),
    ];
}
