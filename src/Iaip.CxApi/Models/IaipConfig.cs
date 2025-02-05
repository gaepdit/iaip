using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Iaip.CxApi.Models;

public record IaipConfig
{
    [UsedImplicitly]
    public string DatabaseIp { get; init; } = "";

    [UsedImplicitly]
    public string DatabasePort { get; init; } = "";

    [UsedImplicitly]
    public string DatabaseUser { get; init; } = "";

    [UsedImplicitly]
    public string DatabasePassword { get; init; } = "";

    [UsedImplicitly]
    public string GoogleMapsApiKey { get; init; } = "";

    [UsedImplicitly]
    public string RaygunApiKey { get; init; } = "";

    [JsonIgnore]
    public string GetConnectionString => new SqlConnectionStringBuilder
    {
        DataSource = $"{DatabaseIp},{DatabasePort}",
        UserID = DatabaseUser,
        Password = DatabasePassword,
        PersistSecurityInfo = true,
        TrustServerCertificate = true,
        InitialCatalog = "airbranch",
    }.ConnectionString;
}
