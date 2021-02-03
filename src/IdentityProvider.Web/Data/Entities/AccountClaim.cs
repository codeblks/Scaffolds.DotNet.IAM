using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace IdentityProvider.Data.Entities
{
    [Table("AccountClaims")]
    public class AccountClaim : IdentityUserClaim<int>
    {
    }
}
