using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IdentityProvider.Data.DbContexts;
using IdentityProvider.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityProvider.Data.Identity
{
    public class AccountStore : Disposable, IUserStore<Account>, IUserEmailStore<Account>,
        IUserPasswordStore<Account>, IUserSecurityStampStore<Account>, IUserRoleStore<Account>
    {
        private readonly IdentityAccountDbContext _context;

        public AccountStore(IdentityAccountDbContext context)
        {
            _context = context;
        }

        #region IUserStore

        public async Task<Account> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            if (int.TryParse(userId, out var id))
            {
                return await _context.Accounts.FindAsync(id);
            }

            return await Task.FromResult((Account)null);
        }

        public async Task<Account> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            return await _context.Accounts.SingleOrDefaultAsync(
                x => x.UserName.ToUpper() == normalizedUserName, cancellationToken) as Account;
        }

        public Task<string> GetUserIdAsync(Account account, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(account.Id.ToString());
        }

        public Task<string> GetNormalizedUserNameAsync(Account user, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            throw new NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(Account user, string normalizedName, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            throw new NotImplementedException();
        }

        public Task<string> GetUserNameAsync(Account account, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            return Task.FromResult(account.UserName);
        }

        public Task SetUserNameAsync(Account account, string userName, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentNullException(nameof(userName));
            }

            account.UserName = userName;
            return Task.CompletedTask;
        }

        public async Task<IdentityResult> CreateAsync(Account account, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            _context.Add(account);

            var result = await _context.SaveChangesAsync(cancellationToken);

            return await Task.FromResult(result == 1 ? IdentityResult.Success : IdentityResult.Failed());
        }

        public async Task<IdentityResult> UpdateAsync(Account account, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            _context.Update(account);

            var result = await _context.SaveChangesAsync(cancellationToken);

            return await Task.FromResult(result == 1 ? IdentityResult.Success : IdentityResult.Failed());
        }

        public async Task<IdentityResult> DeleteAsync(Account account, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            _context.Remove(account);

            var result = await _context.SaveChangesAsync(cancellationToken);

            return await Task.FromResult(result == 1 ? IdentityResult.Success : IdentityResult.Failed());
        }

        #endregion IUserStore

        #region IUserEmailStore

        public async Task<Account> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            return await _context.Accounts.SingleOrDefaultAsync(x =>
               x.Email.ToUpper() == normalizedEmail, cancellationToken) as Account;
        }

        public Task<string> GetEmailAsync(Account account, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            return Task.FromResult(account.Email);
        }

        public Task SetEmailAsync(Account account, string email, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentNullException(nameof(email));
            }

            account.Email = email;
            return Task.CompletedTask;
        }

        public Task<bool> GetEmailConfirmedAsync(Account account, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            return Task.FromResult(account.EmailConfirmed);
        }

        public Task SetEmailConfirmedAsync(Account account, bool confirmed, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            account.EmailConfirmed = confirmed;
            return Task.CompletedTask;
        }

        public Task<string> GetNormalizedEmailAsync(Account user, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            throw new NotImplementedException();
        }

        public Task SetNormalizedEmailAsync(Account user, string normalizedEmail, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            throw new NotImplementedException();
        }

        #endregion IUserEmailStore

        #region IUserPasswordStore

        public Task<bool> HasPasswordAsync(Account account, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            return Task.FromResult(!string.IsNullOrEmpty(account.PasswordHash));
        }

        public Task<string> GetPasswordHashAsync(Account account, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            return Task.FromResult(account.PasswordHash);
        }

        public Task SetPasswordHashAsync(Account account, string passwordHash, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            if (string.IsNullOrWhiteSpace(passwordHash))
            {
                throw new ArgumentNullException(nameof(passwordHash));
            }

            account.PasswordHash = passwordHash;
            return Task.CompletedTask;
        }

        #endregion IUserPasswordStore

        #region IUserSecurityStampStore

        public Task<string> GetSecurityStampAsync(Account account, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            return Task.FromResult(account.SecurityStamp);
        }

        public Task SetSecurityStampAsync(Account account, string stamp, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            if (string.IsNullOrWhiteSpace(stamp))
            {
                throw new ArgumentNullException(nameof(stamp));
            }

            account.SecurityStamp = stamp;
            return Task.CompletedTask;
        }

        #endregion IUserSecurityStampStore

        #region IUserRoleStore

        public async Task AddToRoleAsync(Account account, string roleName, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentNullException(nameof(roleName));
            }

            var normalizedRole = roleName.ToUpperInvariant();

            var accountRole = account.Roles.SingleOrDefault(x => x.Role.Name.ToUpperInvariant() == normalizedRole);

            if (accountRole != null)
            {
                throw new InvalidOperationException($"Account is already a {roleName}");
            }

            var role = await _context.Roles.SingleOrDefaultAsync(x => x.Name.ToUpperInvariant() == normalizedRole);

            if (role == null)
            {
                throw new InvalidOperationException($"Role {roleName} not found.");
            }

            account.Roles.Add(new AccountRole { AccountId = account.Id, RoleId = role.Id });

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IList<string>> GetRolesAsync(Account account, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            var query = (from accountRole in _context.AccountRoles
                         where accountRole.AccountId == account.Id
                         join role in _context.Roles on accountRole.RoleId equals role.Id
                         select role.Name);

            return await query.ToListAsync();
        }

        public async Task<IList<Account>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentNullException(nameof(roleName));
            }

            var accounts = _context.AccountRoles
                .Where(x => x.Role.Name == roleName)
                .Select(x => x.Account);

            return await accounts.ToListAsync(cancellationToken);
        }

        public Task<bool> IsInRoleAsync(Account account, string roleName, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentNullException(nameof(roleName));
            }

            return Task.FromResult(account.Roles.Any(x => x.Role.Name.ToUpperInvariant() == roleName.ToUpperInvariant()));
        }

        public async Task RemoveFromRoleAsync(Account account, string roleName, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new InvalidOperationException($"Role {roleName} not found.");
            }

            var role = await _context.Roles.SingleOrDefaultAsync(x =>
                x.Name.ToUpperInvariant() == roleName.ToUpperInvariant()
            );

            AccountRole accountRole = null;

            if (role != null)
            {
                var roleId = role.Id;
                var accountId = account.Id;

                accountRole = await _context.AccountRoles.FirstOrDefaultAsync(x =>
                    x.RoleId == roleId && x.AccountId == accountId
                );
            }

            if (accountRole != null)
            {
                account.Roles.Remove(accountRole);
                _context.AccountRoles.Remove(accountRole);
            }

            await _context.SaveChangesAsync(cancellationToken);
        }

        #endregion IUserRoleStore
    }
}
