using System.ComponentModel.DataAnnotations;

namespace ViewModel
{
    public class LoginVM
    {
       [Required(ErrorMessage ="لطفا شماره موبایل خود را وارد کنید")]
       [RegularExpression(@"(^(09|9)[0-9][0-9]\d{7}$)", ErrorMessage = "شماره موبایل وارد شده صحیح نمیباشد")]
       public string UserName { get; set; }
       [Required(ErrorMessage = "لطفا رمز عبور خود را وارد کنید")]
       public string Password { get; set; }
       public bool RememberMe { get; set; } = false;
    }
}
