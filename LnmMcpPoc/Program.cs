using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var assemblyDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;

builder.Logging.AddConsole(o => o.LogToStandardErrorThreshold = LogLevel.Trace);

builder.Services
      .AddHttpContextAccessor()
      .AddMcpServer()
      .WithHttpTransport()
      .WithToolsFromAssembly();

var host = builder.Build();

host.UsePathBase("/ln-markets")
    .UseDefaultFiles()
    .UseStaticFiles();

host.MapMcp("/mcp");

await host.RunAsync();
