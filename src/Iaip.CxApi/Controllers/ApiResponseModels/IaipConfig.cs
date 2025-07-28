using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Iaip.CxApi.Controllers.ApiResponseModels;

[UsedImplicitly(ImplicitUseTargetFlags.Members)]
public record IaipConfig
{
    public required string DatabaseIp { get; init; }
    public required string DatabasePort { get; init; }
    public required string DatabaseUser { get; init; }
    public required string DatabasePassword { get; init; }
    public required string GoogleMapsApiKey { get; init; }
    public required string RaygunApiKey { get; init; }
    public required Guid EmailQueueClientId { get; init; }
    public required string EmailQueueApiKey { get; init; }
    public required string NotificationsApiKey { get; init; }

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
