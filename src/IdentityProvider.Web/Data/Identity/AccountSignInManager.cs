using IdentityProvider.Data.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace IdentityProvider.Data.Identity
{
    public class AccountSignInManager : SignInManager<Account>
    {
        public AccountSignInManager(AccountManager userManager,
            IHttpContextAccessor contextAccessor,
            IUserClaimsPrincipalFactory<Account> claimsFactory,
            IOptions<IdentityOptions> optionsAccessor,
            ILogger<AccountSignInManager> logger,
            IAuthenticationSchemeProvider schemes,
            IUserConfirmation<Account> confirmation)
                : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
        {
        }
    }
}
