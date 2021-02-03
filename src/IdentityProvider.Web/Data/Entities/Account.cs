using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityProvider.Data.Entities
{
    [Table("Accounts")]
    public class Account
    {
        public Account()
        {
            Roles = new List<AccountRole>();
        }

        [Key, Required]
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string SecurityStamp { get; set; }

        public bool IsEnabled { get; set; }

        public bool EmailConfirmed { get; set; }

        public ICollection<AccountRole> Roles { get; set; }
    }
}
