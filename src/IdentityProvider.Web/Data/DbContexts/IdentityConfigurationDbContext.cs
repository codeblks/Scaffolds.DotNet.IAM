using IdentityProvider.Data.Seeds;
using IdentityProvider.Extensions;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Extensions;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace IdentityProvider.Data.DbContexts
{
    public class IdentityConfigurationDbContext : ConfigurationDbContext<IdentityConfigurationDbContext>
    {
        private readonly ConfigurationStoreOptions _storeOptions;

        public IdentityConfigurationDbContext(
            DbContextOptions<IdentityConfigurationDbContext> options,
            ConfigurationStoreOptions storeOptions)
            : base(options, storeOptions)
        {
            _storeOptions = storeOptions;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("security");
            builder.HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SequenceHiLo);
            builder.UseHiLo("IdentityConfigurationDbHiLoSequence".ToSnakeCase(), "security");

            builder.ConfigureClientContext(_storeOptions);
            builder.ConfigureResourcesContext(_storeOptions);

            base.OnModelCreating(builder);

            new IdentityClientDataSeed(builder);
        }
    }
}
