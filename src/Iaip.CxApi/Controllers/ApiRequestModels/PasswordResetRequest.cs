using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Iaip.CxApi.Controllers.ApiRequestModels;

public record PasswordResetRequest
{
    [UsedImplicitly]
    public string? Username { get; init; }

    [JsonIgnore]
    public SqlParameter[] SqlParameters => [new SqlParameter("@username", Username)];
}
