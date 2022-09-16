using System.ComponentModel.DataAnnotations;

namespace EShop.Models
{
    public class Roles
    {
        //public Roles()
        //{
        //    this.UserRole = new HashSet<UserRole>();
        //}
        [Key]
        public int RoleId { get; set; }

        [Required(ErrorMessage = "لطفا نام نقش را وارد کنید")]
        [MaxLength(20,ErrorMessage ="این فیلد حداکثر بیست کاراکتر میباشد")]
        public string RoleName { get; set; }

        public ICollection<UserRole> UserRole { get; set; } 
    }
}
