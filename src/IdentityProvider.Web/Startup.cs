using System;
using System.Reflection;
using IdentityProvider.Data.DbContexts;
using IdentityProvider.Data.Entities;
using IdentityProvider.Data.Identity;
using IdentityProvider.Models;
using IdentityProvider.Services;
using IdentityServer4.Configuration;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IdentityProvider.Web
{
    public class Startup
    {
        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }

        public IWebHostEnvironment Environment { get; }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            var connectionString = Configuration.GetConnectionString("IdentityContextConnection");

            var schemaName = "security";
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            var migrationHistoryName = "__SecurityMigrationsHistory";

            services.AddControllersWithViews();

            services.AddDbContext<IdentityAccountDbContext>(options =>
            {
                options.UseSnakeCaseNamingConvention();
                options.UseNpgsql(connectionString, sql =>
                {
                    sql.MigrationsAssembly(migrationsAssembly);
                    sql.MigrationsHistoryTable(migrationHistoryName, schemaName);
                });
            });
            //.AddDbContext<IdentityConfigurationDbContext>(options =>
            //{
            //    options.UseSnakeCaseNamingConvention();
            //    options.UseNpgsql(connectionString, sql =>
            //    {
            //        sql.MigrationsAssembly(migrationsAssembly);
            //        sql.MigrationsHistoryTable(migrationHistoryName, schemaName);
            //    });
            //})
            //.AddDbContext<IdentityPersistedGrantDbContext>(options =>
            //{
            //    options.UseSnakeCaseNamingConvention();
            //    options.UseNpgsql(connectionString, sql =>
            //    {
            //        sql.MigrationsAssembly(migrationsAssembly);
            //        sql.MigrationsHistoryTable(migrationHistoryName, schemaName);
            //    });
            //});

            services.AddIdentity<Account, Role>(options =>
                {
                    options.SignIn.RequireConfirmedEmail = true;
                })
                .AddUserStore<AccountStore>()
                .AddRoleStore<AccountRoleStore>()
                .AddUserManager<AccountManager>()
                .AddRoleManager<AccountRoleManager>()
                .AddSignInManager<AccountSignInManager>()
                .AddDefaultTokenProviders();

            var is4 = services.AddIdentityServer(options =>
                {
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseSuccessEvents = true;
                    options.UserInteraction.LoginUrl = "/account/login";
                    options.UserInteraction.LogoutUrl = "/account/logout";
                    options.Authentication = new AuthenticationOptions()
                    {
                        // ID server cookie timeout set to 10 hours
                        CookieLifetime = TimeSpan.FromHours(10),
                        CookieSlidingExpiration = true
                    };
                })
                // Enable when testing the database
                //.AddConfigurationStore<IdentityConfigurationDbContext>()
                //.AddOperationalStore<IdentityPersistedGrantDbContext>(options =>
                //{
                //    options.EnableTokenCleanup = true;
                //})
                .AddInMemoryClients(IdentityConfig.GetClients())
                .AddInMemoryApiResources(IdentityConfig.GetApiResources())
                .AddInMemoryIdentityResources(IdentityConfig.GetIdentityResources())
                .AddAspNetIdentity<Account>();

            if (Environment.IsDevelopment())
            {
                is4.AddDeveloperSigningCredential();
            }

            services.AddScoped<IProfileService, ProfileService>();

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "IdentityUI/dist";
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            if (!Environment.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseIdentityServer();

            // app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "api/{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "IdentityUI";

                if (Environment.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
