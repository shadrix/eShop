using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Identity;

public static class AuthorizationExtensions
{
    public static void AddAuthorization(this IServiceCollection services, AuthorizationConfig auth)
    {
        services.AddSingleton<IAuthorizationHandler, ScopeHandler>();
        services
            .AddAuthentication()
            .AddJwtBearer(AuthScheme.Internal, options =>
            {
                options.Authority = auth.Authority;
                options.RequireHttpsMetadata = false;
            })
            .AddJwtBearer(AuthScheme.Site, options =>
            {
                options.Authority = auth.Authority;
                options.Audience = auth.SiteAudience;
                options.RequireHttpsMetadata = false;
            });
        services.AddAuthorization(options =>
        {
            options.AddPolicy(AuthPolicy.AllowEndUserPolicy, policy =>
            {
                    policy.AuthenticationSchemes.Add(AuthScheme.Site);
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim(JwtRegisteredClaimNames.Sub);
            });
            options.AddPolicy(AuthPolicy.AllowClientPolicy, policy =>
            {
                    policy.AuthenticationSchemes.Add(AuthScheme.Internal);
                    policy.Requirements.Add(new DenyAnonymousAuthorizationRequirement());
                    policy.Requirements.Add(new ScopeRequirement());
            });
        });

        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
    }
}