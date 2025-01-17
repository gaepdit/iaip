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
        new("@username", Username),
        new("@userpassword", Password),
    ];
}
