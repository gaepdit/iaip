using Iaip.CxApi.DbHelper;
using Iaip.CxApi.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Bind application settings.
builder.Configuration.GetSection(nameof(AppSettings.IaipConfigOptions)).Bind(AppSettings.IaipConfigOptions);

// Configure DB helper.
if (builder.Configuration.GetValue<bool>("TestDbHelper"))
    builder.Services.AddSingleton<IDbHelper, TestDbHelper>();
else
    builder.Services.AddSingleton<IDbHelper, DbHelper>();

// Add controllers.
builder.Services.AddControllers();

// Configure Swagger/OpenAPI documentation.
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.IgnoreObsoleteActions();
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "Georgia EPD IAIP Connections API",
            Contact = new OpenApiContact
            {
                Name = "Georgia EPD-IT Support",
                Email = builder.Configuration["SupportEmail"],
            },
        });
    });
}

// Build app.
var app = builder.Build();

// Configure API documentation
if (builder.Environment.IsDevelopment())
{
    app.UseSwagger(c => { c.RouteTemplate = "api-docs/{documentName}/openapi.json"; });
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/api-docs/v1/openapi.json", "IAIP Connections API v1");
        c.RoutePrefix = "";
        c.DocumentTitle = "Georgia EPD IAIP Connections API";
    });
}

// Map endpoints.
app.MapControllers();
app.MapGet("/health", () => Results.Ok("OK"));
app.MapGet("/version", () => Results.Ok(new { version = AppSettings.GetVersion() }));

// Make it so.
await app.RunAsync();
