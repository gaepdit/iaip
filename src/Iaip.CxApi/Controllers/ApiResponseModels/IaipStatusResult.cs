namespace Iaip.CxApi.Controllers.ApiResponseModels;

public record IaipStatusResult
{
    public bool Enabled { [UsedImplicitly] get; init; }
    public string MinimumVersion { [UsedImplicitly] get; init; } = "";
}
