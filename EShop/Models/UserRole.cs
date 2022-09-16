using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EShop.Models 
{
    public class UserRole
    {
        [Key, Column(Order = 1)]
        public int UserId { get; set; }

        public virtual Users? Users { get; set; }

        [Key, Column(Order = 2)]
        public int RoleId { get; set; }

        public virtual Roles? Roles { get; set; }

    }
}
