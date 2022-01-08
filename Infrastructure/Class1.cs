namespace Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;



public static class Application
{
    public static async Task Run(string[] args, params Type[] startups)
    {
        try
        {
            await using (WebApplication host = Application.CreateHostBuilder(args, startups).Build())
            {
                host.UseCompositeStartupConfigure(((IEnumerable<Type>) Startups.Default).Concat<Type>((IEnumerable<Type>) startups).ToArray<Type>());
                if (host.IsJobAvailable())
                    await host.RunJobAsync();
                else
                    await host.RunAsync();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine((object) ex);
            Environment.Exit(-87);
        }
    }

    internal static WebApplicationBuilder CreateHostBuilder(
        string[] args,
        params Type[] startups)
    {
        string contentPath = Directory.GetCurrentDirectory();
        WebApplicationBuilder builder = WebApplication.CreateBuilder(new WebApplicationOptions()
        {
            ContentRootPath = contentPath
        });
        builder.Host.UseDefaultServiceProvider((Action<ServiceProviderOptions>) (o => o.ValidateScopes = false));
        builder.Host.ConfigureAppConfiguration((Action<IConfigurationBuilder>) (configurationBuilder => configurationBuilder.Configure(args, contentPath)));
        builder.UseCompositeStartupConfigureServices(((IEnumerable<Type>) Startups.Default).Concat<Type>((IEnumerable<Type>) startups).ToArray<Type>());
        return builder;
    }
}
