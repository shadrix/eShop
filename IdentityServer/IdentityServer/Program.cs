using System.IO;
using IdentityServer;
using IdentityServer4.Quickstart.UI;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var configuration = GetConfiguration();

WebHost.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1);
        services.Configure<AppSettings>(configuration);

        services.AddIdentityServer()
            .AddInMemoryIdentityResources(Config.GetIdentityResources())
            .AddInMemoryApiResources(Config.GetApis())
            .AddInMemoryClients(Config.GetClients())
            .AddTestUsers(TestUsers.Users)
            .AddDeveloperSigningCredential();
    })
    .Configure(app =>
    {
        app.UseDeveloperExceptionPage();
            
        app.UseIdentityServer();
        app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Strict });
        app.UseStaticFiles();
        app.UseRouting();
            
        app.UseAuthentication();
        app.UseAuthorization();
            
        app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
    })
    .Build().Run();
    
    
IConfiguration GetConfiguration()
{
    var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables();

    return builder.Build();
}

public partial class Program
{
    public static IConfiguration AppSettings { get; private set; }
}