using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Reflection;

var builder = Host.CreateApplicationBuilder(args);

var assemblyDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
builder.Configuration
       .AddJsonFile(Path.Combine(assemblyDir, "appsettings.json"), optional: true, reloadOnChange: true)
       .AddJsonFile(Path.Combine(assemblyDir, $"appsettings.{builder.Environment.EnvironmentName}.json"), optional: true, reloadOnChange: true);

builder.Logging.AddConsole(o => o.LogToStandardErrorThreshold = LogLevel.Trace);

builder.Services
      .AddOptions<LnMarketsOptions>()
      .BindConfiguration("LnMarkets")
      .Services
      .AddMcpServer()
      .WithStdioServerTransport()
      .WithToolsFromAssembly();

var host = builder.Build();
await host.RunAsync();
