using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Iaip.CxApi.Controllers.ApiRequestModels;

public record UsernameRequest
{
    [UsedImplicitly]
    public string? Email { get; init; }

    [JsonIgnore]
    public SqlParameter[] SqlParameters => [new SqlParameter("@emailaddress", Email)];
}
