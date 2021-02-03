using System.Collections.Generic;
using IdentityProvider.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace IdentityProvider.Data.Identity
{
    public class AccountRoleManager : RoleManager<Role>
    {
        public AccountRoleManager(IRoleStore<Role> store,
            IEnumerable<IRoleValidator<Role>> roleValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            ILogger<RoleManager<Role>> logger)
                : base(store, roleValidators, keyNormalizer, errors, logger)
        {
        }
    }
}
