using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

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
      .WithHttpTransport()
      //.WithStdioServerTransport()
      .WithToolsFromAssembly();

var host = builder.Build();

host.MapMcp("/ln-markets");

await host.RunAsync();
