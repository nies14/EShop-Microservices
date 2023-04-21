using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOcelot().AddCacheManager(settings => settings.WithDictionaryHandle());

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    var env = hostingContext.HostingEnvironment;
    config.AddJsonFile($"ocelot.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true);
});

var app = builder.Build();




using var loggerFactory = LoggerFactory.Create(loggingBuilder => loggingBuilder
    .SetMinimumLevel(LogLevel.Trace)
    .AddConsole()
    .AddDebug());

ILogger logger = loggerFactory.CreateLogger<Program>();
logger.LogInformation("Example log message");

app.MapGet("/", () => "Hello World!");

await app.UseOcelot();

app.Run();