using Iaip.CxApi.Controllers.ApiResponseModels;
using System.Reflection;

namespace Iaip.CxApi.Settings;

internal static class AppSettings
{
    public static Dictionary<string, IaipConfig> IaipConfigOptions { get; set; } = [];

    public static string GetVersion()
    {
        var entryAssembly = Assembly.GetEntryAssembly();
        var segments = (entryAssembly?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
            .InformationalVersion ?? entryAssembly?.GetName().Version?.ToString() ?? "").Split('+');
        return segments[0] + (segments.Length > 0 ? $"+{segments[1][..Math.Min(7, segments[1].Length)]}" : "");
    }
}
