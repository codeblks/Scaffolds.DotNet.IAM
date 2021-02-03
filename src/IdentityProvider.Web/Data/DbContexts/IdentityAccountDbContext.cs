using IdentityProvider.Data.Entities;
using IdentityProvider.Data.Seeds;
using IdentityProvider.Extensions;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace IdentityProvider.Data.DbContexts
{
    public class IdentityAccountDbContext : DbContext
    {
        public IdentityAccountDbContext(DbContextOptions<IdentityAccountDbContext> options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<AccountRole> AccountRoles { get; set; }

        public DbSet<AccountClaim> AccountClaims { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("security");
            builder.HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SequenceHiLo);
            builder.UseHiLo("IdentityAccountDbHiLoSequence".ToSnakeCase(), "security");

            new AccountDataSeed(builder);
        }
    }
}
