using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EShop.Models
{
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required(ErrorMessage = "لطفا نام و نام خانوادگی خود را وارد کنید")]
        [Display(Name = "نام و نام خانوادگی ")]
        public string Name { get; set; }

        [Required(ErrorMessage = "لطفاشماره موبایل خود را وارد کنید")]
        [Display(Name ="نام کاربری (موبایل)")]
        [RegularExpression(@"(^(09|9)[0-9][0-9]\d{7}$)", ErrorMessage = "شماره موبایل وارد شده صحیح نمیباشد")]
        [Microsoft.AspNetCore.Mvc.Remote("IsUserExist", "Account", AdditionalFields = "UserId", ErrorMessage = "این نام کاربری در سایت موجود است !!! ")]
        public string UserName { get; set; }
   
        [Required(ErrorMessage = "لطفا رمز عبور خود را وارد کنید")]
        [Display(Name = "رمز عبور ")]
        public string Password { get; set; }

        [Required(ErrorMessage = "لطفا تکرار رمز عبور خود را وارد کنید")]
        [Compare("Password", ErrorMessage = "کلمه عبور با تکرارش یگی نیست")]
        [NotMapped]
        [Display(Name = "تکرار رمز عبور ")]
        public string ConfrimePassword { get; set; }

        [Display(Name = " سن")]
        public int? Age { get; set; }
      
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "لطفا ایمیل معتبر وارد کنید")]
        [Display(Name = "ایمیل ")]
        public string? Email { get; set; }
        [Display(Name = "کد پستی ")]
        [DataType(DataType.PostalCode)]
        public string? CodePosti { get; set; }

		[Display(Name = "آدرس ")]
        [Required(ErrorMessage = "لطفا آدرس خود را وارد کنید")]
        public string Address { get; set; }
		public DateTime CreateDate { get; set; } = DateTime.Now;
        public string? ShowCreateDate { get; set; }
        public DateTime ? EditDate { get; set; }
        public string? ShowEditDate { get; set; }
        [Display(Name = "وضعیت ")]
        public bool Active { get; set; } = true;
		public virtual ICollection<UserRole>? UserRole { get; set; }
	}
}
