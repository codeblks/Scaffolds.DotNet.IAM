using System;
using System.Linq;
using System.Security.Claims;
using IdentityModel;
using IdentityProvider.Data.DbContexts;
using IdentityProvider.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityProvider.Data.Seeds
{
    public class DataSeed
    {
        public static void EnsureSeedData(string connectionString)
        {
            var services = new ServiceCollection();
            services.AddLogging();

            //services.AddDbContext<AuthDbContext>(opts =>
            //    opts.UseNpgsql(connectionString));

            //services.AddIdentity<Account, Role>()
            //    .AddDefaultTokenProviders();

            //services.AddTransient<IUserStore<Account>, AccountStore>();
            //services.AddTransient<IRoleStore<Role>, RoleStore>();

            using (var serviceProvider = services.BuildServiceProvider())
            {
                using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var context = scope.ServiceProvider.GetService<IdentityAccountDbContext>();
                    context.Database.Migrate();

                    var manager = scope.ServiceProvider.GetRequiredService<UserManager<Account>>();

                    var admin = manager.FindByNameAsync("admin").Result;

                    if (admin == null)
                    {
                        admin = new Account
                        {
                            UserName = "Admin",
                            Email = "admin@org.com",
                            IsEnabled = true,
                            EmailConfirmed = true
                        };

                        var result = manager.CreateAsync(admin, "admin").Result;

                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        result = manager.AddClaimsAsync(admin, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Admin"),
                            new Claim(JwtClaimTypes.GivenName, "Admin"),
                            new Claim(JwtClaimTypes.FamilyName, "Admin"),
                            new Claim(JwtClaimTypes.Email, "admin@org.com"),
                            new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                            new Claim("enabled", true.ToString(), ClaimValueTypes.Boolean)
                        }).Result;

                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }
                        // Log.Debug("bob created");
                    }
                    else
                    {
                        // Log.Debug("bob already exists");
                    }

                    var alice = manager.FindByNameAsync("alice").Result;
                    if (alice == null)
                    {
                        alice = new Account
                        {
                            UserName = "alice"
                        };
                        var result = manager.CreateAsync(alice, "Pass123$").Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        result = manager.AddClaimsAsync(alice, new Claim[]{
                        new Claim(JwtClaimTypes.Name, "Alice Smith"),
                        new Claim(JwtClaimTypes.GivenName, "Alice"),
                        new Claim(JwtClaimTypes.FamilyName, "Smith"),
                        new Claim(JwtClaimTypes.Email, "AliceSmith@email.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                        new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json)
                    }).Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }
                        // Log.Debug("alice created");
                    }
                    else
                    {
                        // Log.Debug("alice already exists");
                    }

                    
                }
            }
        }

    }
}
