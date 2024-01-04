using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Iaip.CxApi.Controllers.ApiRequestModels;

public record SessionCredentials
{
    [UsedImplicitly]
    public int UserId { get; init; }

    [UsedImplicitly]
    public string? Token { get; init; }

    [UsedImplicitly]
    public string? MachineName { get; init; }

    [UsedImplicitly]
    public string? WindowsUserName { get; init; }

    [UsedImplicitly]
    public string? WindowsDomainName { get; init; }

    [JsonIgnore]
    public SqlParameter[] SqlParameters =>
    [
        new SqlParameter("@userId", UserId),
        new SqlParameter("@token", Token),
        new SqlParameter("@machineName", MachineName),
        new SqlParameter("@windowsUserName", WindowsUserName),
        new SqlParameter("@windowsDomainName", WindowsDomainName),
    ];
}
