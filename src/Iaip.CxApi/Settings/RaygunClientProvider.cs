using Mindscape.Raygun4Net.AspNetCore;

namespace Iaip.CxApi.Settings;

public class RaygunClientProvider : DefaultRaygunAspNetCoreClientProvider
{
    public override RaygunClient GetClient(RaygunSettings settings, HttpContext context)
    {
        var client = base.GetClient(settings, context);
        client.ApplicationVersion = typeof(Program).Assembly.GetName().Version?.ToString(3);

        client.SendingMessage += (_, args) =>
        {
            args.Message.Details.Tags ??= new List<string>();
            args.Message.Details.Tags.Add(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
            args.Message.Details.Tags.Add("CX-API");
        };

        return client;
    }
}
