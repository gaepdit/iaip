using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Iaip.CxApi.Controllers.ApiRequestModels;

public record LoginCredentials
{
    [UsedImplicitly]
    public string? Username { get; init; }

    [UsedImplicitly]
    public string? Password { get; init; }

    [JsonIgnore]
    public SqlParameter[] SqlParameters =>
    [
        new SqlParameter("@username", Username),
        new SqlParameter("@userpassword", Password),
    ];
}
