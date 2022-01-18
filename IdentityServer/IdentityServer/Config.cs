using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new ApiResource[]
            {
                new ApiResource("website.com")
                {
                    Scopes = new List<Scope>
                    {
                        new Scope("website.com")
                    },
                },
                new ApiResource("gamesapi")
                {
                    Scopes = new List<Scope>
                    {
                        new Scope("gamesapi.gamesapi"),
                        new Scope("gamesapi.gamesapibff")
                    },
                },
                new ApiResource("relatedproductsapi")
                {
                    Scopes = new List<Scope>
                    {
                        new Scope("relatedproductsapi.relatedproductsapi"),
                        new Scope("relatedproductsapi.relatedproductsapibff")
                    },
                },
                new ApiResource("ratelimitapi")
                {
                    Scopes = new List<Scope>
                    {
                        new Scope("ratelimitapi.ratelimitapi"),
                        new Scope("ratelimitapi.ratelimitapibff")
                    },
                },
                new ApiResource("cartapi")
                {
                    Scopes = new List<Scope>
                    {
                        new Scope("cartapi.cartapi"),
                        new Scope("cartapi.cartapibff")
                    },
                },
                new ApiResource("orderapi")
                {
                    Scopes = new List<Scope>
                    {
                        new Scope("orderapi.orderapi"),
                        new Scope("orderapi.orderapibff")
                    },
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                new Client
                {
                    ClientId = "pkce_client",
                    ClientName = "MVC PKCE Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    ClientSecrets = {new Secret("acf2ec6fb01a4b698ba240c2b10a0243".Sha256())},
                    RedirectUris = {"http://192.168.1.71:5001/signin-oidc"},
                    AllowedScopes = {"openid", "profile", "website.com"},

                    RequirePkce = true,
                    RequireConsent = false,
                    AllowPlainTextPkce = false
                },
                new Client
                {
                    ClientId = "website_client",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("websitesecret".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { "gamesapi.gamesapibff", "relatedproductsapi.relatedproductsapibff", "ratelimitapi.ratelimitapibff", "cartapi.cartapibff", "orderapi.orderapibff" }
                },
                new Client
                {
                    ClientId = "gamesapi_client",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("gamesapisecret".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { "gamesapi.gamesapi", "relatedproductsapi.relatedproductsapi", "ratelimitapi.ratelimitapi", "cartapi.cartapi", "orderapi.orderapi" }
                },
                new Client
                {
                    ClientId = "relatedproductsapi_client",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("relatedproductsapisecret".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { "gamesapi.gamesapi", "relatedproductsapi.relatedproductsapi", "ratelimitapi.ratelimitapi", "cartapi.cartapi", "orderapi.orderapi" }
                },
                new Client
                {
                    ClientId = "ratelimitapi_client",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("ratelimitapisecret".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { "gamesapi.gamesapi", "relatedproductsapi.relatedproductsapi", "ratelimitapi.ratelimitapi", "cartapi.cartapi", "orderapi.orderapi" }
                },
                new Client
                {
                    ClientId = "cartapi_client",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("cartapisecret".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { "gamesapi.gamesapi", "relatedproductsapi.relatedproductsapi", "ratelimitapi.ratelimitapi", "cartapi.cartapi", "orderapi.orderapi" }
                },
                new Client
                {
                    ClientId = "orderapi_client",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("orderapisecret".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { "gamesapi.gamesapi", "relatedproductsapi.relatedproductsapi", "ratelimitapi.ratelimitapi", "cartapi.cartapi", "orderapi.orderapi" }
                }
            };
        }
    }
}