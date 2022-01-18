using IdentityServer;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

WebHost.CreateDefaultBuilder(args)
    .UseStartup<Startup>()
    .UseUrls("http://*:5002");