using IdentityProvider.Extensions;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace IdentityProvider.Data.DbContexts
{
    public class IdentityPersistedGrantDbContext : PersistedGrantDbContext<IdentityPersistedGrantDbContext>
    {
        public IdentityPersistedGrantDbContext(
            DbContextOptions<IdentityPersistedGrantDbContext> options,
            OperationalStoreOptions storeOptions)
            : base(options, storeOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("security");
            builder.HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SequenceHiLo);
            builder.UseHiLo("IdentityPersistedGrantDbHiLoSequence".ToSnakeCase(), "security");
        }
    }
}
