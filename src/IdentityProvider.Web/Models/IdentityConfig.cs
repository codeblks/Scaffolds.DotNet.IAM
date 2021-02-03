using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace IdentityProvider.Models
{
    internal class IdentityConfig
    {
        internal static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };
        }

        internal static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("service-api", "Service API")
            };
        }

        internal static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "console-app-1",
                    ClientName = "Console Application 1",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedScopes = { "service-api" },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword
                },
                new Client
                {
                    ClientId = "console-app-2",
                    ClientName = "Console Application 2",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedScopes = { "service-api" },
                    AllowedGrantTypes = GrantTypes.ClientCredentials
                },
                new Client
                {
                    ClientId = "angular-app",
                    ClientName = "Angular Web Application",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "service-api"
                    },
                    RedirectUris = new List<string>
                    {
                        "http://localhost:58303/callback.html",
                        "http://localhost:58303/renew-callback.html"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "http://localhost:58303/signout-callback.html"
                    },
                    AllowedCorsOrigins = new List<string>
                    {
                        "http://localhost:58303"
                    },
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false
                },
                new Client
                {
                    ClientId = "asptnet-core-mvc-app",
                    ClientName = "AspNet Core MVC Application",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireConsent = false,
                    RequirePkce = false,
                    RedirectUris = { "http://localhost:59124/signin-oidc" },
                    PostLogoutRedirectUris = { "http://localhost:59124/signout-callback-oidc" },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                }
            };
        }

        internal static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "robert",
                    Password = "helloworld"
                }
            };
        }
    }
}
