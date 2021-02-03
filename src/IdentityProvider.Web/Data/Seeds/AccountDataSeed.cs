using System;
using IdentityProvider.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityProvider.Data.Seeds
{
    internal class AccountDataSeed
    {
        public AccountDataSeed(ModelBuilder builder)
        {
            var adminRole = new Role
            {
                Id = 1,
                Name = "Administrator",
                Description = "Administrator of the system."
            };

            var userRole = new Role
            {
                Id = 2,
                Name = "User",
                Description = "User of the system."
            };

            builder.Entity<Role>()
                .HasData(adminRole, userRole);

            var admin = new Account
            {
                Id = 1,
                UserName = "admin",
                Email = "admin@org.com",
                IsEnabled = true,
                EmailConfirmed = true
            };

            admin.SecurityStamp = Guid.NewGuid().ToString();
            admin.PasswordHash = new PasswordHasher<Account>().HashPassword(admin, "admin");
            

            var user = new Account
            {
                Id = 2,
                UserName = "user",
                Email = "user@org.com",
                IsEnabled = true,
                EmailConfirmed = true
            };

            user.SecurityStamp = Guid.NewGuid().ToString();
            user.PasswordHash = new PasswordHasher<Account>().HashPassword(user, "password");

            builder.Entity<Account>()
                .HasData(admin, user);

            builder.Entity<AccountClaim>()
                .HasData(
                    new AccountClaim
                    {
                        Id = 1,
                        UserId = 1,
                        ClaimType = "name",
                        ClaimValue = "Admin"
                    },
                    new AccountClaim
                    {
                        Id = 2,
                        UserId = 1,
                        ClaimType = "given_name",
                        ClaimValue = "Admin"
                    },
                    new AccountClaim
                    {
                        Id = 3,
                        UserId = 1,
                        ClaimType = "family_name",
                        ClaimValue = "Admin"
                    },
                    new AccountClaim
                    {
                        Id = 4,
                        UserId = 1,
                        ClaimType = "email",
                        ClaimValue = "admin@org.com"
                    },
                    new AccountClaim
                    {
                        Id = 5,
                        UserId = 1,
                        ClaimType = "email_verified",
                        ClaimValue = true.ToString()
                    },
                    new AccountClaim
                    {
                        Id = 6,
                        UserId = 1,
                        ClaimType = "is_enabled",
                        ClaimValue = true.ToString()
                    },
                    new AccountClaim
                    {
                        Id = 7,
                        UserId = 2,
                        ClaimType = "name",
                        ClaimValue = "User"
                    },
                    new AccountClaim
                    {
                        Id = 8,
                        UserId = 2,
                        ClaimType = "given_name",
                        ClaimValue = "User"
                    },
                    new AccountClaim
                    {
                        Id = 9,
                        UserId = 2,
                        ClaimType = "family_name",
                        ClaimValue = "User"
                    },
                    new AccountClaim
                    {
                        Id = 10,
                        UserId = 2,
                        ClaimType = "email",
                        ClaimValue = "user@org.com"
                    },
                    new AccountClaim
                    {
                        Id = 11,
                        UserId = 2,
                        ClaimType = "email_verified",
                        ClaimValue = true.ToString()
                    },
                    new AccountClaim
                    {
                        Id = 12,
                        UserId = 2,
                        ClaimType = "is_enabled",
                        ClaimValue = true.ToString()
                    }
                );

            // Add role to accounts
            builder.Entity<AccountRole>()
                .HasData(
                    new AccountRole 
                    {
                        Id = 1,
                        AccountId = admin.Id, 
                        RoleId = adminRole.Id 
                    },
                    new AccountRole
                    {
                        Id = 2,
                        AccountId = user.Id,
                        RoleId = userRole.Id
                    }
                );
        }
    }
}
