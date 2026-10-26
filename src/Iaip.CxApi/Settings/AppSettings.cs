using Azure.Identity;
using Iaip.CxApi.Controllers.ApiResponseModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace Iaip.CxApi.Settings;

internal static class AppSettings
{
    public static string? Version { get; private set; }
    public static Dictionary<string, IaipConfig> IaipConfigOptions { get; set; } = [];

    public static void LoadAppSettings(this IHostApplicationBuilder builder)
    {
        Version = GetVersion();
        builder.AddKeyVault();
        builder.Configuration.GetSection(nameof(AppSettings.IaipConfigOptions)).Bind(AppSettings.IaipConfigOptions);
    }

    private static string GetVersion()
    {
        var entryAssembly = Assembly.GetEntryAssembly();
        var segments = (entryAssembly?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
            .InformationalVersion ?? entryAssembly?.GetName().Version?.ToString() ?? "").Split('+');
        return segments[0] + (segments.Length > 0 ? $"+{segments[1][..Math.Min(7, segments[1].Length)]}" : "");
    }

    private static void AddKeyVault(this IHostApplicationBuilder builder)
    {
        var keyVaultName = builder.Configuration.GetValue<string>("AzureKeyVaultName");
        if (string.IsNullOrEmpty(keyVaultName)) return;

        var keyVaultUri = new Uri($"https://{keyVaultName}.vault.azure.net");
        builder.Configuration.AddAzureKeyVault(keyVaultUri, new DefaultAzureCredential());
    }
}
