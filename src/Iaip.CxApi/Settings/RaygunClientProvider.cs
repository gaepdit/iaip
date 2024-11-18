using Mindscape.Raygun4Net.AspNetCore;

namespace Iaip.CxApi.Settings;

public static class RaygunClientProvider
{
    public static RaygunClient GetClient(RaygunSettings settings, HttpContext context)
    {
        settings.ApplicationVersion = typeof(Program).Assembly.GetName().Version?.ToString(3);
        var client = new RaygunClient(settings);

        client.SendingMessage += (_, args) =>
        {
            args.Message.Details.Tags ??= new List<string>();
            args.Message.Details.Tags.Add(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
            args.Message.Details.Tags.Add("CX-API");
        };

        return client;
    }
}
