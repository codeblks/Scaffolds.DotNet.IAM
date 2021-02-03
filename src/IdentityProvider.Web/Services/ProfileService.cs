using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityProvider.Data.Entities;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;

namespace IdentityProvider.Services
{
    internal class ProfileService : IProfileService
    {
        private readonly IUserClaimsPrincipalFactory<Account> _claimsFactory;
        private readonly UserManager<Account> _accountManager;

        public ProfileService(
            UserManager<Account> accountManager,
            IUserClaimsPrincipalFactory<Account> claimsFactory)
        {
            _accountManager = accountManager;
            _claimsFactory = claimsFactory;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _accountManager.FindByIdAsync(sub);
            var principal = await _claimsFactory.CreateAsync(user);

            var claims = principal.Claims.ToList();
            claims = claims.Where(claim => context.RequestedClaimTypes.Contains(claim.Type)).ToList();

            // Add custom claims in token here based on user properties or any other source
            claims.Add(new Claim("is_enabled", user.IsEnabled.ToString(), ClaimValueTypes.Boolean));

            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var account = await _accountManager.FindByIdAsync(sub);
            context.IsActive = account != null;
        }
    }
}
