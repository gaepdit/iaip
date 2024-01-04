using Iaip.CxApi.Models;

namespace Iaip.CxApi.Settings;

internal static class AppSettings
{
    public static Dictionary<string, IaipConfig> IaipConfigOptions { get; set; } = new();

    // Raygun client settings
    public static RaygunClientSettings RaygunSettings { get; } = new();

    public class RaygunClientSettings
    {
        public string? ApiKey { get; [UsedImplicitly] init; }
    }
}
