using Admin.Announcement.Models.Configuration;
using Microsoft.AspNetCore.ResponseCaching;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureLogging(Logger);
builder.Configuration.AddSolutionsConfiguration(true);// set vault

// Add services to the container.

builder.Services.Configure<ApplicationConfiguration>(builder.Configuration.GetSection("ApplicationConfiguration"));
var configuratorSettingConfig = builder.Configuration.GetSection("UXConfigurations");
builder.Services.AddControllers()
                .AddJsonOptions(options =>
                   options.JsonSerializerOptions
                   .Converters
                   .Add(new JsonStringEnumConverter()));
builder.Services.Configure<ConfiguratorSettingConfig>(configuratorSettingConfig);
builder.Services.AddSolutionsMiddleware(builder.Configuration);
builder.Services.RegisterMongoDbContext();
builder.Services.AddSwagger();
//need to review
builder.Services.AddSingleton<ILogger, SolutionsLogger>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<IAmbientContext, AmbientContext>();
builder.Services.AddBusinessLayer();
builder.Services.RegisterRepositories();
builder.Services.AddCors(o => o.AddPolicy("AllowCorsPolicy", builder =>
                builder.SetIsOriginAllowed(_ => true)
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials()
            ));
builder.Services.AddSecurity(builder.Configuration);

// Configure the HTTP request pipeline.
var app = builder.Build();
if (!app.Environment.IsEnvironment("Production"))
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseCustomSwagger();
//app.UseRouting();
ApplicationConfiguration appConfig = new ApplicationConfiguration();
if (appConfig.EnableCache)
{
    app.UseResponseCaching();
    app.UseWhen(context => context.Request.Path.Value.Contains("/api/Campaigns/widget"), builder =>
    {
        builder.Use(async (context, next) =>
        {
            context.Response.GetTypedHeaders().CacheControl =
                new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
                {
                    Public = true,
                    MaxAge = TimeSpan.FromSeconds(appConfig.CacheTimeout > 0 ? appConfig.CacheTimeout : 3600),

                };
            context.Response.Headers[Microsoft.Net.Http.Headers.HeaderNames.Vary] =
                new string[] { "Accept-Encoding" };

            var responseCachingFeature = context.Features.Get<IResponseCachingFeature>();
            if (responseCachingFeature != null)
            {
                responseCachingFeature.VaryByQueryKeys = new[] { "region", "languageCode", "audience" };
            }

            await next();
        });
    });
}
app.UseSolutionsSecurity();
app.UseCors("AllowCorsPolicy");
app.UseStickyRouteMiddleware();
app.UseSolutionsMiddleware();
app.MapControllers();
app.MapSolutionsHealthChecks();
app.Run();

static void Logger(WebHostBuilderContext ctx, ILoggingBuilder logging)
{
    if (ctx == null || logging == null)
    {
        return;
    }
    logging.ClearProviders();
    logging.AddSerilog();

}
