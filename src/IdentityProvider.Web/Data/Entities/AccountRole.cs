using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityProvider.Data.Entities
{
    [Table("accounts_roles")]
    public class AccountRole
    {
        [Key, Required]
        public int Id { get; set; }

        [Required, ForeignKey(nameof(Account))]
        public int AccountId { get; set; }

        [Required, ForeignKey(nameof(Role))]
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }

        public virtual Account Account { get; set; }
    }
}
