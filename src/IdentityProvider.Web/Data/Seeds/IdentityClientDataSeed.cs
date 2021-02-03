using System;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using static IdentityModel.OidcConstants;

namespace IdentityProvider.Data.Seeds
{
    internal class IdentityClientDataSeed
    {
        public IdentityClientDataSeed(ModelBuilder builder)
        {
            builder.Entity<ApiResource>()
                .HasData(new ApiResource
                {
                    Id = 1,
                    Name = "hr-service-api",
                    DisplayName = "Human Resources Service API"
                });

            builder.Entity<ApiScope>()
                .HasData(new ApiScope
                {
                    Id = 1,
                    Name = "hr-service-api",
                    DisplayName = "Human Resources Service API",
                    Description = null,
                    Required = false,
                    Emphasize = false,
                    ShowInDiscoveryDocument = true,
                    ApiResourceId = 1
                });

            builder.Entity<IdentityResource>()
                .HasData(
                    new IdentityResource()
                    {
                        Id = 1,
                        Enabled = true,
                        Name = "openid",
                        DisplayName = "Your user identifier",
                        Description = null,
                        Required = true,
                        Emphasize = false,
                        ShowInDiscoveryDocument = true,
                        Created = DateTime.UtcNow,
                        Updated = null,
                        NonEditable = false
                    },
                    new IdentityResource()
                    {
                        Id = 2,
                        Enabled = true,
                        Name = "profile",
                        DisplayName = "User profile",
                        Description = "Your user profile information (first name, last name, etc.)",
                        Required = false,
                        Emphasize = true,
                        ShowInDiscoveryDocument = true,
                        Created = DateTime.UtcNow,
                        Updated = null,
                        NonEditable = false
                    }
                );

            builder.Entity<IdentityClaim>()
                .HasData(
                    new IdentityClaim
                    {
                        Id = 1,
                        IdentityResourceId = 1,
                        Type = "sub"
                    },
                    new IdentityClaim
                    {
                        Id = 2,
                        IdentityResourceId = 2,
                        Type = "email"
                    },
                    new IdentityClaim
                    {
                        Id = 4,
                        IdentityResourceId = 2,
                        Type = "given_name"
                    },
                    new IdentityClaim
                    {
                        Id = 5,
                        IdentityResourceId = 2,
                        Type = "family_name"
                    },
                    new IdentityClaim
                    {
                        Id = 6,
                        IdentityResourceId = 2,
                        Type = "name"
                    }
                );


            builder.Entity<Client>()
                .HasData(
                    new Client
                    {
                        Id = 1,
                        Enabled = true,
                        ClientId = "console-app-1",
                        ClientName = "Console Application 1",
                        ProtocolType = "oidc",
                        RequireClientSecret = true,
                        RequireConsent = false,
                        Description = null,
                        AllowRememberConsent = false,
                        AlwaysIncludeUserClaimsInIdToken = false,
                        RequirePkce = false,
                        AllowAccessTokensViaBrowser = false,
                        AllowOfflineAccess = false
                    },
                    new Client
                    {
                        Id = 2,
                        Enabled = true,
                        ClientId = "console-app-2",
                        ClientName = "Console Application 2",
                        ProtocolType = "oidc",
                        RequireClientSecret = true,
                        RequireConsent = false,
                        Description = null,
                        AllowRememberConsent = true,
                        AlwaysIncludeUserClaimsInIdToken = false,
                        RequirePkce = false,
                        AllowAccessTokensViaBrowser = false,
                        AllowOfflineAccess = false
                    },
                    new Client
                    {
                        Id = 3,
                        Enabled = true,
                        ClientId = "angular-app",
                        ProtocolType = "oidc",
                        RequireClientSecret = false,
                        RequireConsent = true,
                        ClientName = "Angular Web Application",
                        Description = null,
                        AllowRememberConsent = true,
                        AlwaysIncludeUserClaimsInIdToken = false,
                        RequirePkce = true,
                        AllowAccessTokensViaBrowser = true,
                        AllowOfflineAccess = true
                    },
                    new Client
                    {
                        Id = 4,
                        Enabled = true,
                        ClientId = "aspnet-core-mvc-app",
                        ClientName = "AspNet Core MVC Application",
                        ProtocolType = "oidc",
                        RequireClientSecret = false,
                        RequireConsent = true,
                        Description = null,
                        AllowRememberConsent = true,
                        AlwaysIncludeUserClaimsInIdToken = false,
                        RequirePkce = false,
                        AllowAccessTokensViaBrowser = false,
                        AllowOfflineAccess = true
                    }
                );

            builder.Entity<ClientGrantType>()
                .HasData(
                    new ClientGrantType
                    {
                        Id = 1,
                        GrantType = GrantTypes.ClientCredentials,
                        ClientId = 1
                    },
                    new ClientGrantType
                    {
                        Id = 2,
                        GrantType = GrantTypes.Password,
                        ClientId = 2
                    },
                    new ClientGrantType
                    {
                        Id = 3,
                        GrantType = GrantTypes.AuthorizationCode,
                        ClientId = 3
                    },
                    new ClientGrantType
                    {
                        Id = 4,
                        GrantType = GrantTypes.AuthorizationCode,
                        ClientId = 4
                    }
                );

            builder.Entity<ClientScope>()
                .HasData(
                    new ClientScope
                    {
                        Id = 1,
                        Scope = "hr-service-api",
                        ClientId = 3
                    },
                    new ClientScope
                    {
                        Id = 2,
                        Scope = "hr-service-api",
                        ClientId = 2
                    },
                    new ClientScope
                    {
                        Id = 3,
                        Scope = IdentityServerConstants.StandardScopes.OpenId,
                        ClientId = 3
                    },
                    new ClientScope
                    {
                        Id = 4,
                        Scope = IdentityServerConstants.StandardScopes.Profile,
                        ClientId = 3
                    },
                    new ClientScope
                    {
                        Id = 5,
                        Scope = "hr-service-api",
                        ClientId = 3
                    }
                    ,
                    new ClientScope
                    {
                        Id = 6,
                        Scope = IdentityServerConstants.StandardScopes.OpenId,
                        ClientId = 4
                    }
                    ,
                    new ClientScope
                    {
                        Id = 7,
                        Scope = IdentityServerConstants.StandardScopes.Profile,
                        ClientId = 4
                    }
                    ,
                    new ClientScope
                    {
                        Id = 8,
                        Scope = "hr-service-api",
                        ClientId = 4
                    }
                );

            builder.Entity<ClientSecret>()
                .HasData(
                    new ClientSecret
                    {
                        Id = 1,
                        Value = "secret".ToSha256(),
                        Type = "SharedSecret",
                        ClientId = 1
                    },
                    new ClientSecret
                    {
                        Id = 2,
                        Value = "secret".ToSha256(),
                        Type = "SharedSecret",
                        ClientId = 2
                    }
                );

            builder.Entity<ClientPostLogoutRedirectUri>()
                .HasData(
                    new ClientPostLogoutRedirectUri
                    {
                        Id = 1,
                        PostLogoutRedirectUri = "http://localhost:5002/signout-callback-oidc",
                        ClientId = 3
                    },
                    new ClientPostLogoutRedirectUri
                    {
                        Id = 2,
                        PostLogoutRedirectUri = "http://localhost:5003/index.html",
                        ClientId = 4
                    }
                );

            builder.Entity<ClientRedirectUri>()
                .HasData(
                    new ClientRedirectUri
                    {
                        Id = 1,
                        RedirectUri = "http://localhost:5002/signin-oidc",
                        ClientId = 3
                    },
                    new ClientRedirectUri
                    {
                        Id = 2,
                        RedirectUri = "http://localhost:5003/callback.html",
                        ClientId = 4
                    }
                );

            builder.Entity<ClientCorsOrigin>()
                .HasData(
                    new ClientCorsOrigin
                    {
                        Id = 1,
                        Origin = "http://localhost:49608",
                        ClientId = 3
                    }
                );
        }
    }
}
