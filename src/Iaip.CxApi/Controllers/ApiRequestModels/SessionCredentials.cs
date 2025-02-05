using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Iaip.CxApi.Controllers.ApiRequestModels;

public record SessionCredentials
{
    [UsedImplicitly]
    [JsonRequired]
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
        new("@userId", UserId),
        new("@token", Token),
        new("@machineName", MachineName),
        new("@windowsUserName", WindowsUserName),
        new("@windowsDomainName", WindowsDomainName),
    ];
}
