using Iaip.CxApi.DbHelper;
using Iaip.CxApi.Settings;
using Microsoft.OpenApi.Models;
using Mindscape.Raygun4Net.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Bind application settings.
builder.Configuration.GetSection(nameof(AppSettings.IaipConfigOptions)).Bind(AppSettings.IaipConfigOptions);

// Configure application error monitoring.
builder.Configuration.GetSection(nameof(AppSettings.RaygunSettings)).Bind(AppSettings.RaygunSettings);
if (!string.IsNullOrEmpty(AppSettings.RaygunSettings.ApiKey))
{
    builder.Services.AddRaygun(builder.Configuration, options =>
    {
        options.ApiKey = AppSettings.RaygunSettings.ApiKey;
    });
}

// Configure DB helper.
if (builder.Configuration.GetValue<bool>("TestDbHelper"))
    builder.Services.AddSingleton<IDbHelper, TestDbHelper>();
else
    builder.Services.AddSingleton<IDbHelper, DbHelper>();

// Add controllers.
builder.Services.AddControllers();

// Configure Swagger/OpenAPI documentation.
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

// Build app.
var app = builder.Build();

// Configure API documentation
app.UseSwagger(c => { c.RouteTemplate = "api-docs/{documentName}/openapi.json"; });
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/api-docs/v1/openapi.json", "IAIP Connections API v1");
    c.RoutePrefix = "api-docs";
    c.DocumentTitle = "Georgia EPD IAIP Connections API";
});

// Configure the HTTP request pipeline.
if (!string.IsNullOrEmpty(AppSettings.RaygunSettings.ApiKey)) app.UseRaygun();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Make it so.
app.Run();
